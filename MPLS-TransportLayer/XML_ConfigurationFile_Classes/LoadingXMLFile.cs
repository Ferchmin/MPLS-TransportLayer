using System;
using System.IO;
using System.Xml.Serialization;

/*
 * Klasa odpowiadająca za serializację (zapis) oraz deserializację (odczyt)
 * danych z pliku konfiguracyjnego XML.
 * - klasa posiada jedynie zmienne oraz metody statyczne;
 * - nie potrzeba tworzyć obiektu tej klasy;
 */

namespace MPLS_TransportLayer.Packet_Classes
{
    public class LoadingXMLFile
    {
        public static ConfigXMLFile dataSource { get; private set; }

        /*
         * Metoda odpowiedzialna za odczytywanie danych z pliku konfiguracyjnego
         * - metoda tworzy klasę ConfigXMLFile na podstawie pliku ConfigXMLFile.xml
         */
        public static ConfigXMLFile Deserialization(string configFilePath)
        {
            object obj = new object();
            XmlSerializer deserializer = new XmlSerializer(typeof(ConfigXMLFile));
            try
            {
                using (TextReader reader = new StreamReader(configFilePath))
                {
                    obj = deserializer.Deserialize(reader);
                }
                return obj as ConfigXMLFile;
            }
            catch (Exception e)
            {
                DeviceClass.MakeLog("ERROR - Deserialization cannot be complited.");
                return null;
            }
        }

        /*
         * Metoda odpowiedzialna za zapisywanie danych do pliku konfiguracyjnego
         * - metoda zapisuję dane z klasy ConfigXMLFile do pliku ConfigXMLFile.xml
         */
        public static void Serialization(string configFilePath, ConfigXMLFile dataSource)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigXMLFile));
            try
            {
                using (TextWriter writer = new StreamWriter(configFilePath, false))
                {
                    serializer.Serialize(writer, dataSource);
                }
            }
            catch (Exception e)
            {
                DeviceClass.MakeLog("ERROR - Serialization cannot be complited.");
            }
        }
    }
}
