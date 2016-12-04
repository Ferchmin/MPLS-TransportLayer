using System;
using System.IO;

/*
 * Klasa odpowiadająca za działanie całego urządzenia.
 * - instancja tej klasy jest tworzona w Program, nastepnie wszystko co się dzieje przechodzi tutaj
*/

namespace MPLS_TransportLayer
{
    class DeviceClass
    {
        private string _filePath;
        private ConfigurationClass _configDataSource;
        private PortsClass _socket;
        private ForwardingClass _forwardingManager;

        public ForwardingClass ForwardingManager
        {
            get { return _forwardingManager; }
        }


        /*
         * Konstruktor
         * - wczytuje z konsoli ścieżkę do pliku konfiguracyjnego;
         * - inicjalizuje wszystkie lokalne zmienne
         * - uruchamia pracę urządzenia
        */
        public DeviceClass()
        {
            //wczytywanie ścieżki pliku konfiguracyjnego
            ReadConfigFilePath();

            //inicjowanie lokalnych zmiennych
            _configDataSource = new ConfigurationClass(_filePath);
            _forwardingManager = new ForwardingClass
                (_configDataSource.ForwardingTable, _configDataSource.IpToPortTable);


            _socket = new PortsClass(this, _configDataSource.MyIpAddress, _configDataSource.MyPortNumber);
            

            //uruchomienie pracy urządzenia
            StartWorking();
        }

        /*
        * Metoda odpowiedzialna za bezpieczne odczytanie ściezki do pliku.
        * - metoda sprawdza, czy dany plik istnieje, jak nie to pyta ponownie
       */
        private void ReadConfigFilePath()
        {
            do
            {
                Console.WriteLine("Podaj ścieżkę pliku konfiguracyjnego");
                _filePath = Console.ReadLine();

            } while (!File.Exists(_filePath));
            

            //sprawdzenie, czy dany plik istnieje
            //Console.WriteLine(File.Exists(curFile) ? "File exists." : "File does not exist.");
        }

        /*
         * Główna metoda programu.
        */
        public void StartWorking()
        {
            string end = null;
            Console.WriteLine("Program działa - aby wyłączyć wpisz end.");
            do
            {
                end = Console.ReadLine();
            }
            while (end != "end");
        }
    }
}
