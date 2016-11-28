using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*
 * Klasa odpowiedzialna za wszystkie operacje związane z gniazdem
 * - na każde urzadzenie w sieci przypada jeden oddzielny Socket
*/

namespace MPLS_TransportLayer
{
    class PortsClass
    {
        Socket cloudSocket;
        IPEndPoint cloudIpEndPoint;

        IPEndPoint receivingIPEndPoint;
        EndPoint receivingEndPoint;
        IPEndPoint receivedIPEndPoint;

        byte[] buffer;
        byte[] packet;

        public PortsClass()
        {
            string myIpAddress = "127.0.0.1";
            int myPort = 8888;

            //tworzymy gniazdo i przypisujemy mu numer portu i IP zgodne z plikiem konfig
            cloudSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            cloudIpEndPoint = new IPEndPoint((IPAddress.Parse(myIpAddress)), myPort);
            cloudSocket.Bind(cloudIpEndPoint);

            //tworzymy punkt końcowy, z którego będziemy odbierali dane (z jakiegokolwiek interfejsu i z jakiegokolwiek portu)
            receivingIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            receivingEndPoint = (EndPoint)receivingIPEndPoint;

            //tworzymy bufor nasłuchujący
            buffer = new byte[1024];

            //nasłuchujemy
            cloudSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref receivingEndPoint, new AsyncCallback(ReceivedPacket), null);
        }

        public void ReceivedPacket(IAsyncResult res)
        {
            //kończę odbieranie danych
            int size = cloudSocket.EndReceiveFrom(res, ref receivingEndPoint);

            //tworzę tablicę bajtów składającą się jedynie z danych otrzymanych (otrzymany pakiet)
            byte[] receivedPacket = new byte[size];
            Array.Copy(buffer, receivedPacket, receivedPacket.Length);

            //tworzę tymczasoyw punkt końcowy zawierający informacje o nadawcy (jego ip oraz nr portu)
            receivedIPEndPoint = (IPEndPoint)receivingEndPoint;

            //tworzę logi
            Console.WriteLine("Otrzymaliśmy pakiet od: " + receivedIPEndPoint.Address + " port " + receivedIPEndPoint.Port);
            Console.WriteLine("Pakieto to: " + Encoding.UTF8.GetString(receivedPacket));

            //przesyłam pakiet do metody przetwarzającej
            ProcessReceivedPacket(receivedPacket);

            //inicjuje start wysyłania przetworzonego pakietu do nadawcy
            cloudSocket.BeginSendTo(packet, 0, packet.Length, SocketFlags.None, receivingEndPoint, new AsyncCallback(SendPacket), null);

            //zeruje bufor odbierający
            buffer = new byte[1024];

            //uruchamiam ponowne nasłuchiwanie
            cloudSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref receivingEndPoint, new AsyncCallback(ReceivedPacket), null);
        }


        public void SendPacket(IAsyncResult res)
        {
            int size = cloudSocket.EndSendTo(res);
            Console.WriteLine("Wysłaliśmy pakiet do: " + receivedIPEndPoint.Address + " port " + receivedIPEndPoint.Port);
            Console.WriteLine("Pakieto to: " + Encoding.UTF8.GetString(packet));
        }


        private void ProcessReceivedPacket(byte[] receivedPacket)
        {
            //w celach testowych przypisuje ten sam pakiet co przyszedł do wysłania
            packet = receivedPacket;
        }
    }
}
