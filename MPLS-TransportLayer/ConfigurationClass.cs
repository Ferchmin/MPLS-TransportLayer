using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Klasa odpowiedzialna za wczytywanie pliku konfiguracyjnego
 * - ściezka do pliku jest wpisywana przez użytkownika za pomocą konsoli
 * - pliki zapisane są w postaci .xml 
 * 
 * LOKALNE ZMIANY W KLASIE
 * - odczytanie tablicy z pliku
 * - stworzenie lokalnej tablicy przekierowania
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
         */
        public string FilePath { get; private set; }
        public Dictionary<string, string> ForwardingTable;


        /*
         * Konstruktor klasy
         * - przypisanie ścieżki do pliku odczytanej z konsoli;
         */
        public ConfigurationClass()
        {

        }

        /*
        * Klasa odpowiadająca za bezpieczne otwarcię pliku
        */
        private void OpenFile()
        {

        }

        /*
         * Klasa odpowiadająca za odczytanie pliku w odpowiedni sposób
         * - odczytanie tablicy i wpisanie jej do słownika
         */
        private void ReadFile()
        {

        }

        /*
        * Klasa odpowiadająca za bezpieczne zamknięcie pliku
        */
        private void CloseFile()
        {

        }
    }
}
