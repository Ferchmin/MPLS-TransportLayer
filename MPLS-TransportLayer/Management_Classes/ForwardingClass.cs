using MPLS_TransportLayer.Packet_Classes;
using System;
using System.Collections.Generic;
using System.Net;

/*
 * Klasa odpowiedzialna za przekierowywanie pakietów
 * - znalezienie dopasowania
 * - zmiana nagłówka chmuryKablowej
 * - przekazanie pakietu do wysłania
 * 
 * - znalezienie dopasowania portu do ip docelowego
*/

namespace MPLS_TransportLayer
{
    class ForwardingClass
    {
        /*
         * Lokalne zmienne
         * - _inputString, czyli string wejściowy (IP_węzła + interfejs_węzła)
         * - _outputString, czyli string wyjściowy (IP_węzłaDocelowego + interfejs_węzłaDocelowego)
         * - _cloudHeader, czyli nagłówek chmury kablowej
         */
        private string _inputString;
        private string _outputString;
        private int _cloudHeader;

        private Dictionary<string, string> _forwardingTable;
        private Dictionary<string, string> _ipToPortTable;

        private byte[] _finalPacket;
        private IPEndPoint _destinationPoint;

        /*
         * Konstruktor klasy
         */
        public ForwardingClass
            (Dictionary<string, string> forwardingTable, Dictionary<string, string> ipToPortTable)
        {
            _forwardingTable = forwardingTable;
            _ipToPortTable = ipToPortTable;
        }

        /*
         * Metoda odpowiedzialna za prawidłowy forwarding pakietów
         * - pełny scenariusz funkcjonalności programu (threadName to nazwa wątpku wywołującego)
         * - wyodrębnij nagłówek startowy
         * - stworzenie stringa wejściowego
         * - wyszukanie stringa wyjściowego pasującego do stringa wejściowego
         * - zmiana nagłówka chmury na podstawie stringa wyjściowego
         * - stworzenie punktu końcowego
         * - przekierowanie pakietu do odpowiedniego wątku na podstawie stringa wyjściowego
         * 
         */
        public byte[] MakeForward(byte[] receivedPacket, ref IPEndPoint destinationIpEndPoint)
        {
            _cloudHeader = 0;
            _inputString = null;
            _outputString = null;
            _finalPacket = null;
            _destinationPoint = null;

            MPLSPacket packet = new MPLSPacket(receivedPacket);

            GetCloudHeader(packet);
            MakeInputString(destinationIpEndPoint.Address.ToString(), _cloudHeader);
            FindPair();
            if (_outputString != null)
            {
                ChangeHeader(packet);
                ForwardPacket(packet);
                if (destinationIpEndPoint != null)
                {
                    CreateDestinationPort(GetFromString(_outputString, 1), ref destinationIpEndPoint);
                    return _finalPacket;
                }
                else
                    return null;
            }
            else
                return null;   
        }

        /*
         * Klasa odpowiedzialna za odczytanie wartości nagłówka z otrzymanego pakietu
         * oraz przypisanie wartości nagłówka do zmiennej 
        */
        private void GetCloudHeader(MPLSPacket packet)
        {
            packet.ReadCloudHeader();
            _cloudHeader = packet.SourceInterface;
        }

        /*
         * Klasa odpowiedzialna za generowanie odpowiedniego napisu wejściowego
         * - wynikiem jest obiekt typu string ( id_wątku_wywołującego + nagłówek pakietu)
        */
        private void MakeInputString(string sourceIP, int sourceInterface)
        {
            _inputString = sourceIP + "-" + sourceInterface;
        }

        /*
         * Metoda przeszukuje słownik
         * - czy istnieje taki klucz w słowniku
         * - jezeli nie to zrob jakies czynnosci
         * - jezeli tak to wez jego wartosc i przypisz do zmiennej OutputString
        */
        private void FindPair()
        {
            if (_forwardingTable.ContainsKey(_inputString))
                _forwardingTable.TryGetValue(_inputString, out _outputString);
            else
            {
                //tworzę logi
                DeviceClass.MakeLog("ERROR - Cannot find the value in ForwardingTable of key:" + _inputString);
            }
        }

        /*
         * Metoda zmienia wartośc nagłówka w pakiecie wyjściowym
        */
        private void ChangeHeader(MPLSPacket packet)
        {
            packet.ChangeCloudHeader(ushort.Parse(GetFromString(_outputString, 2)));
        }

        /*
         * Metoda wysyła gotowy pakiet do konkretnego wątku
        */
        private void ForwardPacket(MPLSPacket packet)
        {
            _finalPacket = packet.Packet;
        }

        /*
         * Metoda tworzy punkt końcowy na podstawie docelowego adresu IP i wartości w tablicy _ipToPortTable
        */
        private void CreateDestinationPort(string destinationIp, ref IPEndPoint destinationIpEndPoint)
        {
            string tmpOut;

            if (_ipToPortTable.ContainsKey(destinationIp))
            {
                _ipToPortTable.TryGetValue(destinationIp, out tmpOut);
                destinationIpEndPoint = new IPEndPoint((IPAddress.Parse(destinationIp)), Int32.Parse(tmpOut));
            }
            else
            {
                //tworzę logi
                DeviceClass.MakeLog("ERROR - Cannot find the value in IpToPortTable of key:" + destinationIp);
            }
        }

        /*
         * Metoda wyjmuję z łączonego stinga element pierwszy albo drugi
        */
        private string GetFromString(string value, int index)
        {
            string[] tmp = value.Split('-');
            return tmp[index-1];
        }
    }
}
