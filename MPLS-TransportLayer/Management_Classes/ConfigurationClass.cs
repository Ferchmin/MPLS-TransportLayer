using MPLS_TransportLayer.Packet_Classes;
using System.Collections.Generic;

/*
 * Klasa odpowiedzialna za wczytywanie pliku konfiguracyjnego
 * - ściezka do pliku jest wpisywana przez użytkownika za pomocą konsoli
 * - pliki zapisane są w postaci .xml 
 * 
 * LOKALNE ZMIANY W KLASIE
 * - odczytanie tablicy z pliku
 * - stworzenie lokalnej tablicy przekierowania
 * - stworzenie tablicy skojarzeń adresów ip z ich programistycznymi portami
*/

namespace MPLS_TransportLayer
{
    class ConfigurationClass
    {
        /*
         * Lokalne zmienne
         * - FilePath - string do ściezki z plikiem
         * - ForwardingTable - słownik łączący klucz z wartością (key, value) 
         *      klucz - string zawierający informacje wejściowe (ID węzła + przesłany w nagłówku numer interfejsu np: 1-2)
         *      wartość - string zawierający informacje wyjściowe (ID węzła docelowego + nowy nr portu wysyłany w nagłówku np: 2-2)
         * - IpToPortTable - słownik zawierający adres IP i numer portu, który jest przypisany do danego elementu sieciowego
         * (jest on potrzebny do prawidłowego stworzenia punktów końcowych)
         */
        private string _filePath;
        private ConfigXMLFile _configFile;

        private string _myIpAddres;
        private int _myPortNumber;
        private Dictionary<string, string> _forwardingTable;
        private Dictionary<string, string> _ipToPortTable;

        public string FilePath
        {
            get { return _filePath; }
        }
        public string MyIpAddress
        {
            get { return _myIpAddres; }
        }
        public int MyPortNumber
        {
            get { return _myPortNumber; }
        }
        public Dictionary<string, string> ForwardingTable
        {
            get { return _forwardingTable; }
        }
        public Dictionary<string, string> IpToPortTable
        {
            get { return _ipToPortTable; }
        }


        /*
         * Konstruktor klasy
         * - przypisanie ścieżki do pliku odczytanej z konsoli;
         */
        public ConfigurationClass(string filePath)
        {
            _filePath = filePath;
            _configFile = null;
            _myIpAddres = null;
            _myPortNumber = 0;
            _forwardingTable = new Dictionary<string, string>();
            _ipToPortTable = new Dictionary<string, string>();

            PreparaFile();
            ReadFile();
        }
        
        /*
        * Klasa odpowiadająca za przygotowanie programu do wczytywania
        * - tworzymy instancję klasy konfiguracyjnej
        */
        private void PreparaFile()
        {
            _configFile = new ConfigXMLFile();
        }

        /*
         * Klasa odpowiadająca za odczytanie pliku w odpowiedni sposób
         * - dokonujemy deserializacji pliku xml
         * - przypisujemy zmienne z klasy konfiguracyjnej do lokalnych zmiennych
         */
        private void ReadFile()
        {
            //deserializacja pliku
            _configFile = LoadingXMLFile.Deserialization(_filePath);

            //ustawiamy adres ip i numer portu chmury kablowej
            _myIpAddres = _configFile.XML_myIPAddress;
            _myPortNumber = _configFile.XML_myPortNumber;

            //tworzymy wpisy do słownika forwardingu
            foreach(ForwardingTableRecord record in _configFile.XML_ForwardingTable)
            {
                string keyData;
                string valueData;

                keyData = record.XML_sourceIPAddress + "-" + record.XML_sourceInterfaceNumber;
                valueData = record.XML_destinationIPAddress + "-" + record.XML_destinationInterfaceNumber;

                _forwardingTable.Add(keyData, valueData);
            }

            //tworzymy wpisy do słownika punktów końcowych
            foreach (IpToPortTableRecord record in _configFile.XML_IpToPortTable)
            {
                string keyData;
                string valueData;

                keyData = record.XML_IpAddress;
                valueData = record.XML_PortNumber;

                _ipToPortTable.Add(keyData, valueData);
            }
        }
    }
}
