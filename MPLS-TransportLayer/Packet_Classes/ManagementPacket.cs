using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Klasa odpowiedzialna za tworzenie i odczytywanie pakietów w domenie zarządzania.
 * - tworzymy pakiet w określonej strukturze
 * - odczytujemy z niego odpowiednie nagłówki
 * - tworzymy tablice bajtów możliwą do wysłania przez sieć 
 * 
 * ----------------
 * Management Packet Structure
 * ----------------
 * HeaderName    -> | MANAGEMENT_HEADER |         IP_HEADER          |  DATA_HEADER   |       DATA       |
 * Description   -> |  Data_Identifier  | IP_Source | IP_Destination | MESSAGE_LENGTH |     MESSAGE      |
 * Size in bytes -> |       1B          |     4B    |      4B        |       1B       | MESSAGE_LENGTH B |
 *                                   
 *                                
 * MANAGEMENT_HEADER: ma ustaloną długość 1 bajtów - Source_Interface może mieć zakres 0-255! (wartość ushort)
 * IP_HEADER:    ma ustaloną długość 8 bajtów      - przenosi adresy IP zapisane np 127001 - (127.0.0.1) każde pole ma swój bajt (ushort)
 * DATA_HEADER:  ma ustaloną długość 1 bajta       - określa, ile bajtów wiadomości przesyłamy w pakiecie (ushort)
 * DATA:         ma zmienną długość 0-255 bajtów   - przenosi wiadomość użytkową (string max 255 znaków)
 * 
 *                       
 *                       
 * OBIEKT TEJ KLASY TWORZONY JEST ZA KAŻDYM RAZEM, GDY CHCEMY UTWORZYĆ LUB ODCZYTAC (ZMODYFIKOWAC) DANE!
*/

namespace MPLS_TransportLayer.Packet_Classes
{
    class ManagementPacket : PacketStructure
    {
        #region Private Variables
        private ushort _dataIdentifier;
        private string _ipSource;
        private string _ipDestination;
        private ushort _messageLength;
        private string _data;
        #endregion


        #region Public Properties

        public ushort DataIdentifier
        {
            get { return _dataIdentifier; }
            set { _dataIdentifier = value; }
        }
        public string IpSource
        {
            get { return _ipSource; }
            set { _ipSource = value; }
        }
        public string IpDestination
        {
            get { return _ipDestination; }
            set { _ipDestination = value; }
        }
        public ushort MessageLength
        {
            get { return _messageLength; }
            set { _messageLength = value; }
        }
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion

        /* 
         * Typ wyliczeniowy, określający rodzaj wysłanej wiadomości
         * - DataIdentifier to index DataType
        */
        enum DataType
        {
            isUp, keepAlive, command, response
        };


        #region CreatingNewPacket
        /*
         * Konsruktor służący do stworzenia pakietu
         * - przypisanie wartości domyślnych (samych zer)
         * - maksymalny rozmiar pakietu ustawione na 275 BAJTY
         * (255 wiadomość + 1 długość_wiadomosci + 8 adresyIP + 1 management_Header)
         */
        public ManagementPacket()
            :base(265)
        {
            _dataIdentifier = 0;
            _ipSource = null;
            _ipDestination = null;
            _messageLength = 0;
            _data = null;
        }
        /* 
         * Metoda tworząca cały pakiet 
         * - metoda przeznaczona dla typu command i response
        */
        public byte[] CreatePacket(ushort dataId, string ipScr, string ipDst, ushort messageLength, string message)
        {
            //zapisujemy nagłówek zarzadzania
            WriteOneByte(dataId, 0);

            //dodaje ip źrodła
            string[] parsingTable;
            parsingTable = ipScr.Split('.');
            WriteOneByte(ushort.Parse(parsingTable[0]), 1);
            WriteOneByte(ushort.Parse(parsingTable[1]), 2);
            WriteOneByte(ushort.Parse(parsingTable[2]), 3);
            WriteOneByte(ushort.Parse(parsingTable[3]), 4);

            //dodaje ip docelowe
            parsingTable = ipDst.Split('.');
            WriteOneByte(ushort.Parse(parsingTable[0]), 5);
            WriteOneByte(ushort.Parse(parsingTable[1]), 6);
            WriteOneByte(ushort.Parse(parsingTable[2]), 7);
            WriteOneByte(ushort.Parse(parsingTable[3]), 8);

            //dodaję nagłówek danych oraz same dane
            WriteOneByte(messageLength, 9);
            WriteString(message, 10);

            if (Error)
            {
                Console.WriteLine("Podano błędne dane - dane poza zakresem!");
                return null;
            }
            else
                return EndMakingPacket();
        }
        /* 
         * Metoda tworząca cały pakiet 
         * - metoda dla typu isUp i keepAlive
        */
        public byte[] CreatePacket
            (ushort dataId, string ipScr, string ipDst)
        {
            //zapisujemy nagłówek zarzadzania
            WriteOneByte(dataId, 0);

            //dodaje ip źrodła
            string[] parsingTable;
            parsingTable = ipScr.Split('.');
            WriteOneByte(ushort.Parse(parsingTable[0]), 1);
            WriteOneByte(ushort.Parse(parsingTable[1]), 2);
            WriteOneByte(ushort.Parse(parsingTable[2]), 3);
            WriteOneByte(ushort.Parse(parsingTable[3]), 4);

            //dodaje ip docelowe
            parsingTable = ipDst.Split('.');
            WriteOneByte(ushort.Parse(parsingTable[0]), 5);
            WriteOneByte(ushort.Parse(parsingTable[1]), 6);
            WriteOneByte(ushort.Parse(parsingTable[2]), 7);
            WriteOneByte(ushort.Parse(parsingTable[3]), 8);

            //dodaję zerową długośc wiadomości i pusty string
            WriteOneByte(0, 9);
            WriteString(String.Empty, 10);

            if (Error)
            {
                Console.WriteLine("Podano błędne dane - dane poza zakresem!");
                return null;
            }
            else
                return EndMakingPacket();
        }
        #endregion

        #region ReadingFromPacket
        /*
         * Konsruktor służący do odczytania zawartości pakietu
         * - przypisanie referencji do pakietu do lokalnej zmiennej
         * - wyzerowanie wartości lokalnych
         */
        public ManagementPacket(byte[] receivedPacket)
            : base(receivedPacket)
        {
            _dataIdentifier = 0;
            _ipSource = null;
            _ipDestination = null;
            _messageLength = 0;
            _data = null;
        }
        public void ReadHolePacket()
        {
            ReadManagementHeader();
            ReadIpHeader();
            ReadDataHeader();
            ReadData();
        }
        public void ReadManagementHeader()
        {
            _dataIdentifier = ReadOneByte(0);
        }
        public void ReadIpHeader()
        {
            //reading ip source
            ushort tmp_value;
            for (int i = 1; i < 5; i++)
            {
                tmp_value = ReadOneByte(i);
                _ipSource += tmp_value.ToString();
                if (i != 4) _ipSource += ".";
            }

            //reading ip destination
            for (int i = 5; i < 9; i++)
            {
                tmp_value = ReadOneByte(i);
                _ipDestination += tmp_value.ToString();
                if (i != 8) _ipDestination += ".";
            }
        }
        public void ReadDataHeader()
        {
            _messageLength = ReadOneByte(9);
        }
        public void ReadData()
        {
            _data = ReadString(10, _messageLength);
        }
        #endregion


        #region EditingExistingPacket
        #endregion
    }
}
