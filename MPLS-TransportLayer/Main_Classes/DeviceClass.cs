using System;
using System.IO;
using System.Linq;

/*
 * Klasa odpowiadająca za działanie całego urządzenia.
 * - instancja tej klasy jest tworzona w Program, nastepnie wszystko co się dzieje przechodzi tutaj
*/

namespace MPLS_TransportLayer
{
    class DeviceClass
    {
        private static string _fileLogPath;
        private static int _logID;

        private string _fileConfigurationPath;
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
            //inicjalizacja ustalonej ściezki pliku zawierającego historię zdarzeń
            _fileLogPath = "LogDatabase.txt";

            //odczytujemy wartość ID ostatniego zdarzenia zapisanego w pliku
            InitializeLogLastIdNumber();

            //LOG
            MakeLog("Inicjalizacja wartości rozruchowych programu.");

            //wczytywanie ścieżki pliku konfiguracyjnego
            ReadConfigFilePath();

            //inicjowanie lokalnych zmiennych
            _configDataSource = new ConfigurationClass(_fileConfigurationPath);
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
                _fileConfigurationPath = Console.ReadLine();

            } while (!File.Exists(_fileConfigurationPath));
        }

        /*
         * Metoda odpowiedzialna za zapisywanie zdarzeń występujących w programie.
         * - wszystkie zdarzenia są oprócz tego, że wyświetlane na konsoli
         * to sa również przechowywane w pliku o nazwie LogDatabase.txt
        */
        public static void MakeLog(string logDescription)
        {
            string log;

            using (StreamWriter file = new StreamWriter(_fileLogPath, true))
            {
                log = _logID + " | " + DateTime.Now.ToString("hh:mm:ss") + " " + logDescription;
                file.WriteLine(log);
                _logID++;
            }

            Console.WriteLine(log);
        }

        /*
        * Metoda pozwalająca odczytać z histori zdarzeń ostatni wpis.
        * - dzięki temu, możliwa jest kontynuacja zapisywania histori zdarzeń w jednym pliku;
        * - id jest zawsze oddzielone | od reszty logu;
        * - jeżeli plik nie istnieje to zaczynamy od 1
       */
        private void InitializeLogLastIdNumber()
        {
            if (File.Exists(_fileLogPath))
            {
                string last = File.ReadLines(_fileLogPath).Last();
                string[] tmp = last.Split('|');
                _logID = Int32.Parse(tmp[0]);
                _logID++;
            }
            else
                _logID = 1;
        }



        /*
         * Główna metoda programu.
        */
        public void StartWorking()
        {
            MakeLog("INFO - Start working...");

            Console.WriteLine("Program działa - aby wyłączyć wpisz end.");
            Console.WriteLine("<-------------------------------------->");
            string end = null;
            do
            {
                end = Console.ReadLine();
            }
            while (end != "end");

            //LOG
            DeviceClass.MakeLog("INFO - Stop working...");
        }
    }
}
