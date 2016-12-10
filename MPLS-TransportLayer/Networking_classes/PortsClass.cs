using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

/*
 * Klasa odpowiedzialna za wszystkie operacje związane z gniazdem
 * - na każde urzadzenie w sieci przypada jeden oddzielny Socket
*/

namespace MPLS_TransportLayer
{
    class PortsClass
    {
        #region Private Variables
        private Socket _cloudSocket;
        private IPEndPoint _cloudIpEndPoint;

        private IPEndPoint _receivingIPEndPoint;
        private EndPoint _receivingEndPoint;

        private byte[] _buffer;
        private int _bufferSize;

        private string _myIpAddress;
        private int _myPort;

        private DeviceClass _cloud;
        #endregion

        /*
		* Konstruktor - wymaga podania zmiennych pobranych z pliku konfiguracyjnego
		*/
        public PortsClass(DeviceClass cloud, string myIpAddress, int myPort)
        {
            InitializeData(cloud, myIpAddress, myPort);
            InitializeSocket();
        }

        /*
		* Metoda odpowiedzialna za przypisanie danych do lokalnych zmiennych.
		*/
        private void InitializeData(DeviceClass cloud,string myIpAddress, int myPort)
        {
            _cloud = cloud;
            _myIpAddress = myIpAddress;
            _myPort = myPort;

            _bufferSize = 275;
        }

        /*
		* Metoda odpowiedzialna za inicjalizację nasłuchiwania na przychodzące wiadomośći.
		*/
        private void InitializeSocket()
        {
            try
            {
                //tworzymy gniazdo i przypisujemy mu numer portu i IP zgodne z plikiem konfig
                _cloudSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _cloudIpEndPoint = new IPEndPoint((IPAddress.Parse(_myIpAddress)), _myPort);
                _cloudSocket.Bind(_cloudIpEndPoint);
            }
            catch
            {
                //LOG
                DeviceClass.MakeLog("ERROR - Incorrect IP address or port number or these values are already in use.");
                _cloud.StopWorking("Incorrect IP address or port number or these values are already in use.");
            }

            //LOG
            DeviceClass.MakeLog("INFO - Cloud Socket: IP:" + _myIpAddress + " Port:" + _myPort);

            //tworzymy punkt końcowy, z którego będziemy odbierali dane (z jakiegokolwiek interfejsu i z jakiegokolwiek portu)
            _receivingIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _receivingEndPoint = (EndPoint)_receivingIPEndPoint;

            //tworzymy bufor nasłuchujący
            _buffer = new byte[_bufferSize];

            //nasłuchujemy
            _cloudSocket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _receivingEndPoint, new AsyncCallback(ReceivedPacket), null);

            //LOG
            DeviceClass.MakeLog("INFO - Start Listening.");
        }

        /*
		* Metoda odpowiedzialna za odbieranie wiadomości
        * - kończymy odbieranie wiadomości
        * - uruchamiamy nasłuchiwanie od nowa
        * - tworzymy logi zdarzeń
        * - wysyłamy pakiet do analizy
        * - odsyłamy pakiet w nowe miejsce
		*/
        public void ReceivedPacket(IAsyncResult res)
        {
            int size;
            try
            {
                //kończę odbieranie danych
                size = _cloudSocket.EndReceiveFrom(res, ref _receivingEndPoint);
            }
            catch
            {
                IPEndPoint unreachableHost = _receivingEndPoint as IPEndPoint;
                DeviceClass.MakeLog("ERROR - Cannnot send packet to: IP:" + unreachableHost.Address + " Port: " + unreachableHost.Port + ". Destination unreachable (Port unreachable)");

                //ustawiam odpowiedni recivingEndPoint
                _receivingIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                _receivingEndPoint = (EndPoint)_receivingIPEndPoint;

                //uruchamiam ponowne nasłuchiwanie
                _cloudSocket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _receivingEndPoint, new AsyncCallback(ReceivedPacket), null);

                return;
            }

            //tworzę tablicę bajtów składającą się jedynie z danych otrzymanych (otrzymany pakiet)
            byte[] receivedPacket = new byte[size];
            Array.Copy(_buffer, receivedPacket, receivedPacket.Length);

            //tworzę tymczasowy LOKALNY punkt końcowy zawierający informacje o nadawcy (jego ip oraz nr portu)
            IPEndPoint _receivedIPEndPoint = (IPEndPoint)_receivingEndPoint;

            //zeruje bufor odbierający
            _buffer = new byte[_bufferSize];

            //ustawiam odpowiedni recivingEndPoint
            _receivingIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _receivingEndPoint = (EndPoint)_receivingIPEndPoint;

            //uruchamiam ponowne nasłuchiwanie
            _cloudSocket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _receivingEndPoint, new AsyncCallback(ReceivedPacket), null);

            //tworzę logi
            DeviceClass.MakeLog("INFO - Received packet from: IP:" + _receivedIPEndPoint.Address + " Port: " + _receivedIPEndPoint.Port);

            //tworze instancję do wysłania i przekazuję pakiet do metody przetwarzającej
            //przekazuję pakiet do metody przetwarzającej
            //metoda zmienia mi referęcję lokalnego punktu końcowego na docelową
            byte[] destinationPacket = null;
            destinationPacket = ProcessReceivedPacket(receivedPacket, ref _receivedIPEndPoint);


            if ((destinationPacket != null) && (_receivedIPEndPoint != null))
                //inicjuje start wysyłania przetworzonego pakietu do nadawcy
                _cloudSocket.BeginSendTo(destinationPacket, 0, destinationPacket.Length, SocketFlags.None, _receivedIPEndPoint, new AsyncCallback(SendPacket), _receivedIPEndPoint);
            else
                DeviceClass.MakeLog("ERROR - Cannot send packet");

        }

        /*
		* Metoda odpowiedzialna za wysyłanie wiadomości
        * - kończymy wysyłanie wiadomości
        * - tworzymy logi zdarzen
		*/
        public void SendPacket(IAsyncResult res)
        {
            var endPoint = res.AsyncState as IPEndPoint;
            int size = _cloudSocket.EndSendTo(res);

            //tworzę logi
            DeviceClass.MakeLog("INFO - Packet send to: IP:" + endPoint.Address + " Port: " + endPoint.Port);

        }

        /*
		* Metoda odpowiedzialna za przetwarzanie pakietu
        * - przyjmuje otrzymany pakiet oraz ip nadawcy
        * - przekazuję dane do obiektu dklasy ForwardingClass
        * - metoda zwraca finalny pakiet gotowy do wysłania
		*/
        private byte[] ProcessReceivedPacket(byte[] receivedPacket, ref IPEndPoint destinationIpEndPoint)
        {
            return _cloud.ForwardingManager.MakeForward(receivedPacket, ref destinationIpEndPoint);
        }
    }
}
