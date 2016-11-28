using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Klasa odpowiedzialna za tworzenie i odczytywanie pakietów.
 * - tworzymy pakiet w określonej strukturze
 * - odczytujemy z niego odpowiednie nagłówki
 * - tworzymy tablice bajtów możliwą do wysłania przez sieć 
 * 
 * ----------------
 * Packet Structure
 * ----------------
 * HeaderName    -> |  CLOUD_HEADER  |                MPLS_HEADER             |            INFORMATION_HEADER                    |
 * Description   -> |  ID |   PORT   |  MPLS_BODY_NUMBER |     MPLS_LABEL     |  MESSAGE_LENGTH    |           MESSAGE           |
 * Size in bytes -> |  1  |    2     |         1         |         1          |         2          |       MESSAGE_LENGTH        |
 *                     0    1    2             3                   4*              5*         6*           7*                *...
 * 
 * CLOUD_HEADER:       ma ustaloną długość 3 bajtów
 *                       - ID ma domyślnie 1 bajt -> czyli możemy nadawać etykiety ze zbioru 0-255!
 *                       - PORT ma domyślnie 2 bajty -> czyli możemy nadawać na portach ze zbioru 0-65535!
 * MPLS_HEADER:        ma zmienną długość w zależności od liczby etykiet na stosie (dla jednej etykiety ma 2 bajty)
 *                       - MPLS_BODY_NUMBER -> 1 bajt mówiący o liczbi etykiet na stosie (0-255!)
 *                       - MPLS_LABEL  -> 1 bajt mówiący o etykiecie (0-255!) MOŻE SIĘ POWIELAć
 * INFORMATION_HEADER: ma zmienną długość 2bajty + 0 lub tyle, ile jest wpisane w message_length
 *                       - MESSAGE_LENGTH ma domyslnie 2 baty -> czyli możemy przesłać wiadomośc o długości 0-65535 bajtów!  
 *                       - MESSAGE -> tyle bajtów wiadomości użytkowej ile podane w message_length 
 *                       
 *                       
 * OBIEKT TEJ KLASY TWORZONY JEST ZA KAŻDYM RAZEM, GDY CHCEMY UTWORZYĆ LUB ODCZYTAC (ZMODYFIKOWAC) DANE!
 * 
 * Jeżeli w programie tworzymy pakiet (węzeł kliencki) to używamy metody CreatePacket i żadnej więcej! (nie dodawałem wyjątków)
 * Jeżeli pakiet odebraliśmy i chcemy z niego odczytać dane użyteczne (węzeł kliencki) lub chcemy odczytac i zmodyfikować kawałek
 * to uzywamy odpowiednich funkcji po kolei (na początku powinniśmy przypisać odebrany pakiet do zmiennej ReceivedPacket!!!!)
*/

namespace MPLS_TransportLayer
{
    class PacketClass
    {
        #region Private Variables
        private int id;
        private int port;
        private int mpls_body_number;
        private int[] mpls_label;
        private int message_length;
        private string message;
        private byte[] receivedPacket;
        private bool orderChecked;
        #endregion

        #region Public Properties
        public int NetworkElementID
        {
            get { return id; }
            set { id = value; }
        }
        public int InputPortNumber
        {
            get { return port; }
            set { port = value; }
        }
        public int MPLSBodyNumber
        {
            get { return mpls_body_number; }
            set { mpls_body_number = value; }
        }
        public int[] MPLSLabel
        {
            get { return mpls_label; }
            set { mpls_label = value; }
        }
        public int MessageLength
        {
            get { return message_length; }
            set { message_length = value; }
        }
        public string MessageData
        {
            get { return message; }
            set { message = value; }
        }
        public byte[] ReceivedPacket
        {
            get { return receivedPacket; }
            set { receivedPacket = value; }
        }
        #endregion

        /*
         * Konsruktor domyslny
         * - przypisanie wartości domyślnych (samych zer)
         */
        public PacketClass()
        {
            this.id = 0;
            this.port = 0;
            this.mpls_body_number = 0;
            this.mpls_label = null;
            this.message_length = 0;
            this.message = null;
            this.receivedPacket = null;

            //sprawdzenie kolejności odczytywania
            this.orderChecked = false;
        }


        /* TEST +
         * Metoda tworzy cały pakiet od zera
         * - potrzebuje wszystkie elementy pakietu aby zwrócić tablicę bajtów gotowych do wysłania
         * - długoś wiadomości jest wyliczana w tej metodzie
        */
        public byte[] CreatePacket(int id, int port, int mpls_body_number, int[] mpls_label, string message)
        {
            List<byte> newPacket = new List<byte>();
            int message_length = Encoding.UTF8.GetByteCount(message);

            //cloudHeader
            newPacket.Add((byte)id);
            newPacket.AddRange(BitConverter.GetBytes((Int16)port));

            //mplsHEADER+body
            newPacket.Add((byte)mpls_body_number);
            for (int i = 0; i < mpls_body_number; i++)
                newPacket.Add((byte)mpls_label[i]);

            //informationHeader + body
            newPacket.AddRange(BitConverter.GetBytes((Int16)message_length));
            newPacket.AddRange(Encoding.UTF8.GetBytes(message));

            return newPacket.ToArray(); ;
        }


        #region READERS (zakładamy, że przypisaliśmy wcześniej pakiet do zmiennej ReceivedPacket !!)
        /*
        * Metoda sprawdzająca, czy bajty czytać od prawej czy lewej
       */
        private void ChangeOrder(byte[] receivedPacket)
        {
            /*
             * true - little-endian;
             * false - big-endian;
             * Little-endian oznacza, że najbardziej znaczący bajt znajduje się na prawym końcu tablicy
             * Big-endian oznacza, że najbardziej znaczący bajt znajduje się na lewym końcu tablizy
            */
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(receivedPacket);
                orderChecked = true;
            }
        }

        /* TEST +
         * Metoda odczytuje nagłówek CLOUD
         * - parametrem jest otrzymany drogą sieciową pakiet, czyli tablica bajtów
         * - metoda ustawia parametry lokalne, do których trzeba będzie się odwołać w dalszej części
        */
        public void ReadCloudHeader()
        {
            if (!orderChecked)
                //ChangeOrder(receivedPacket);

                //oczytujemy ID zapisane na 1 bajcie
                id = receivedPacket[0];

            //odczytujemy PORT zapisany na 2 bajtach (czyli tyle ile ma Int16 2*8-16)
            //tablica bajtów + startIndex
            port = BitConverter.ToInt16(receivedPacket, 1);

        }

        /* TEST +
         * Metoda odczytuje nagłówek MPLS
         * - potrzebuje otrzymaną droga sieciową tablicę bajtów
        */
        public void ReadMPLSHeader()
        {
            if (!orderChecked)
                //ChangeOrder(receivedPacket);

                mpls_body_number = receivedPacket[3];
            mpls_label = new int[mpls_body_number];
            for (int i = 0; i < mpls_body_number; i++)
                mpls_label[i] = receivedPacket[4 + i];
        }

        /* TEST +
         * Metoda odczytuje nagłówek INFORMATION
         * - potrzebuje otrzymaną droga sieciową tablicę bajtów
         * - zakładam, że jak dane przychodzą do klienta to mają tylko jedną etykietę końcową!
        */
        public void ReadInformationHeader()
        {
            //if (!orderChecked)
            //ChangeOrder(receivedPacket);

            message_length = BitConverter.ToInt16(receivedPacket, 5);
            message = Encoding.UTF8.GetString(receivedPacket, 7, message_length);
        }
        #endregion


        #region EDITORS (zakładamy, że przypisaliśmy wcześniej pakiet do zmiennej ReceivedPacket !!)
        /* TEST +
         * Metoda podmienia nagłówek CLOUD
         * - potrzebujemy nowe ID, nowy port;
        */
        public void ChangeCloudHeader(int newID, int newPort)
        {
            List<byte> newPacket = new List<byte>();
            newPacket.Add((byte)newID);
            newPacket.AddRange(BitConverter.GetBytes((Int16)newPort));

            //bierzemy wszystko oprócz pierwszych 3 bajtów oznaczających nagłówek chmury kablowej
            byte[] rest = new byte[receivedPacket.Length - 3];
            Array.Copy(receivedPacket, 3, rest, 0, receivedPacket.Length - 3);
            newPacket.AddRange(rest);

            receivedPacket = newPacket.ToArray();
        }

        /*
         * Metoda podmienia nagłówek MPLS
         * - potrzebujemy nową etykietę oraz pakiet
         * - command to POP lub PUSH lub SWAP
         * - Push – add a label ( w domyśle następny, 2 i wyższy) – Swap – replace the label (zamień ten który jest pierwszy od lewej) – Pop – remove the label (usuń jeden z lewej)
         * - newBigDataStore to przygotowana lista bajtów zawierająca 
        */
        public void ChangeMPLSHeader(string command, int newMPLSLabel, byte[] newBigDataStore)
        {
            switch (command)
            {
                case "POP":

                    break;
                case "PUSH":
                    Console.WriteLine("Case 2");
                    break;
                case "SWAP":
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
        #endregion

    }
}
