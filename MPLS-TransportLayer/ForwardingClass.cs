using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Klasa odpowiedzialna za przekierowywanie pakietów
 * - skonstruowanie stringa wejściowego
 * - sprawdzenie, czy istnieje dopasowany string wyjściowy
 * - zamiana nagłówka
 * - wysłanie do odpowiedniego wątku zarzadzającego
*/

namespace MPLS_TransportLayer
{
    class ForwardingClass
    {
        /*
         * Lokalne zmienne
         * - InputString, czyli string wejściowy (ID_węzła + port_węzła)
         * - OutputString, czyli string wyjściowy (ID_węzłaDocelowego + port_węzłaDocelowego)
         * - CloudHeader, czyli nagłówek chmury kablowej (wartość portu węzła)
         */
        public string InputString { get; private set; }
        public string OutputString { get; private set; }
        public int CloudHeader { get; private set; }

        /*
        * Lokalne pomocnicze
        * - ForwardingTable, czyli referencja do tabicy forwardingu stworzonej przez obiekt ConfigurationClass
        * - ThreadsTable, czyli słownik id z odpowiedzialnym dla tych id wątkami
        */
        private Dictionary<string, string> ForwardingTable;
        private Dictionary<int, int> ThreadsTable;

        /*
         * Konstruktor klasy
         * 
         */
        public ForwardingClass()
        {

        }

        /*
         * Klasa wywoływana przez każdy wątek, jeżeli dany wątek wykryje przychodzące dane
         * - pełny scenariusz funkcjonalności programu (threadName to nazwa wątpku wywołującego)
         * - wyodrębnij nagłówek startowy
         * - stworzenie stringa wejściowego
         * - wyszukanie stringa wyjściowego pasującego do stringa wejściowego
         * - zmiana nagłówka chmury na podstawie stringa wyjściowego
         * - przekierowanie pakietu do odpowiedniego wątku na podstawie stringa wyjściowego
         * 
         * Trzeba zastanowić się w jakiej postaci dociera pakiet
         */
        private void MakeForward(string threadName, byte[] packet)
        {

            GetCloudHeader(packet);
            MakeInputString(threadName);
            FindPair();
            ChangeHeader();
            ForwardPacket();
        }


        /*
        * Klasa odpowiedzialna za odczytanie wartości nagłówka z otrzymanego pakietu
        * oraz przypisanie wartości nagłówka do zmiennej 
        */
        private void GetCloudHeader(byte[] packet)
        {

        }

        /*
        * Klasa odpowiedzialna za generowanie odpowiedniego napisu wejściowego
        * - wynikiem jest obiekt typu string ( id_wątku_wywołującego + nagłówek pakietu)
        */
        private void MakeInputString(string threadName)
        {

        }

        /*
       * Klasa przeszukuje słownik
       * - czy istnieje taki klucz w słowniku
       * - jezeli nie to zrob jakies czynnosci
       * - jezeli tak to wez jego wartosc i przypisz do zmiennej OutputString
       */
        private void FindPair()
        {

        }

        /*
       * Klasa zmienia wartośc nagłówka w pakiecie wyjściowym
       */
        private void ChangeHeader()
        {

        }

        /*
       * Klasa wysyła gotowy pakiet do konkretnego wątku
       */
        private void ForwardPacket()
        {

        }
    }
}
