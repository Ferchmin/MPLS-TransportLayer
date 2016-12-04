using System.Collections.Generic;
using System.Xml.Serialization;

/*
 * Klasa opisująca strukturę pliku konfiguracyjnego xml.
 */

namespace MPLS_TransportLayer.Packet_Classes
{
    [XmlRoot("CloudConfiguration")]
    public class ConfigXMLFile
    {
        [XmlElement("myIPAddress")]
        public string XML_myIPAddress { get; set; }

        [XmlElement("myPortNumber")]
        public int XML_myPortNumber { get; set; }

        [XmlArray("ForwardingTable")]
        [XmlArrayItem("Record", typeof(ForwardingTableRecord))]
        public List<ForwardingTableRecord> XML_ForwardingTable { get; set; }

        [XmlArray("IpToPortTable")]
        [XmlArrayItem("Record", typeof(IpToPortTableRecord))]
        public List<IpToPortTableRecord> XML_IpToPortTable { get; set; }

        public ConfigXMLFile()
        {
            XML_ForwardingTable = new List<ForwardingTableRecord>();
            XML_IpToPortTable = new List<IpToPortTableRecord>();
        }
    }

    public struct ForwardingTableRecord
    {
        [XmlElement("sourceIPAddress")]
        public string XML_sourceIPAddress { get; set; }

        [XmlElement("sourceInterfaceNumber")]
        public int XML_sourceInterfaceNumber { get; set; }

        [XmlElement("destinationIPAddress")]
        public string XML_destinationIPAddress { get; set; }

        [XmlElement("destinationInterfaceNumber")]
        public int XML_destinationInterfaceNumber { get; set; }
    }

    public struct IpToPortTableRecord
    {
        [XmlElement("ipAddress")]
        public string XML_IpAddress { get; set; }

        [XmlElement("portNumber")]
        public string XML_PortNumber { get; set; }
    }

}
/*
 * Klasa opisuje nastepujący plik:
 <CloudConfiguration>
 <myPortNumber>8888</myPortNumber>
 <ForwardingTable>
   <Record>
     <sourceIPAddress>1000</sourceIPAddress>
     <sourceInterfaceNumber>1</sourceInterfaceNumber>
     <destinationIPAddress>2000</destinationIPAddress>
     <destinationInterfaceNumber>2</destinationInterfaceNumber>
   </Record>
   <Record>
     <sourceIPAddress>2000</sourceIPAddress>
     <sourceInterfaceNumber>2</sourceInterfaceNumber>
     <destinationIPAddress>1000</destinationIPAddress>
     <destinationInterfaceNumber>1</destinationInterfaceNumber>
   </Record>
 </ForwardingTable>
 <IpToPortTable>
   <Record>
       <ipAddress>127.0.0.1</ipAddress>
       <portNumber>6001</portNumber>
   </Record>
   <Record>
       <ipAddress>127.0.0.2</ipAddress>
       <portNumber>6002</portNumber>
   </Record
 </IpToPortTable
</CloudConfiguration>

*/

