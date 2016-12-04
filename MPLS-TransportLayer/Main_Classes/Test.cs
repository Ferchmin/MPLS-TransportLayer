using MPLS_TransportLayer.Packet_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPLS_TransportLayer
{
    class Test
    {
        string end = null;
        static byte[] packet = null;

        public Test()
        {
            do
            {
                Console.WriteLine("Wybierz numer testu: ");
                string test = Console.ReadLine();

                switch (test)
                {
                    case "1":
                        Test_CreatePacket();
                        break;
                    case "2":
                        Test_ReadHolePacket();
                        break;
                    case "3":
                        Test_ChangeAll();
                        break;
                    case "4":
                        Test_ManagementPacketCreate();
                        break;
                    case "5":
                        Test_ManagementPacketRead();
                        break;
                    case "6":
                        Test_ManagementPacketChange();
                        break;
                    case "7":
                        Test_Deserialization();
                        break;
                    case "8":
                        Test_ConfigurationClass();
                        break;
                }

                Console.WriteLine("...");
                end = Console.ReadLine();
            }
            while (end != "end");
        }



        private void Test_ConfigurationClass()
        {
            ConfigurationClass test = new ConfigurationClass("Cloud_Configuration_File.xml");
        }
        private void Test_Deserialization()
        {
            ConfigXMLFile test = new ConfigXMLFile();
            string path = "Cloud_Configuration_File.xml";

            test = LoadingXMLFile.Deserialization(path);
        }
        private void Test_CreatePacket()
        {
            packet = null;

            MPLSPacket test = new MPLSPacket();
            ushort source_interface = 1;
            ushort s = 1;
            ushort mpls_label = 111;
            string ip_source = "127.0.0.1";
            string ip_destination = "127.0.0.2";

            string data = "Ala Ma Kota";
            ushort data_length = (ushort)data.Length;
            packet = null;

            packet = test.CreatePacket
                (source_interface, s, mpls_label, ip_source, ip_destination, data_length, data);
        }
        private void Test_ReadHolePacket()
        {
            packet = null;

            Test_CreatePacket();

            MPLSPacket test2 = new MPLSPacket(packet);
            test2.ReadHolePacket();
        }
        private void Test_ChangeAll()
        {
            packet = null;

            Test_CreatePacket();

            MPLSPacket test3 = new MPLSPacket(packet);
            test3.ReadHolePacket();


            test3.ChangeCloudHeader(99);
            test3.ChangeMplsHeader(0, 55);

            test3.AddMplsHeader(13, 13);
            test3.DeleteMplsHeader();
        }
        private void Test_ManagementPacketCreate()
        {
            packet = null;

            ManagementPacket test = new ManagementPacket();
            ushort data_identifier = 3;
            string ip_source = "127.0.0.1";
            string ip_destination = "127.0.0.2";
            string data = "komenda";
            ushort message_length = (ushort)data.Length;
            packet = test.CreatePacket(data_identifier, ip_source, ip_destination,message_length, data);

        }
        private void Test_ManagementPacketRead()
        {
            packet = null;
            Test_ManagementPacketCreate();

            ManagementPacket test = new ManagementPacket(packet);
            test.ReadHolePacket();
        }
        private void Test_ManagementPacketChange()
        {

        }



    }
}
