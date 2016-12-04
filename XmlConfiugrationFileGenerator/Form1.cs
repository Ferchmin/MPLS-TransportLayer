using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPLS_TransportLayer.Packet_Classes;

namespace XmlConfiugrationFileGenerator
{
    public partial class Form1 : Form
    {
        private ConfigXMLFile newFile;
        private string configFilePath;

        public Form1()
        {
            InitializeComponent();
            newFile = new ConfigXMLFile();
        }


        private void button_Add_Click(object sender, EventArgs e)
        {
            newFile.XML_ForwardingTable.Add(new ForwardingTableRecord()
            {
                XML_sourceIPAddress = textBox_ForwTabl_IP_Src.Text,
                XML_sourceInterfaceNumber = int.Parse(textBox_ForwTabl_Int_Src.Text),
                XML_destinationIPAddress = textBox_ForwTabl_IP_Dst.Text,
                XML_destinationInterfaceNumber = int.Parse(textBox_ForwTabl_Int_Dst.Text)
            });
            MessageBox.Show("Dodano nowy rekord w tablicy Forwarding");
        }
    



        private void button_tab2_Click(object sender, EventArgs e)
        {
            newFile.XML_IpToPortTable.Add(new IpToPortTableRecord()
            {
                XML_IpAddress = textBox_Tab2.Text,
                XML_PortNumber = textBox_tab2_Port.Text
            });
            MessageBox.Show("Dodano nowy rekord w tablicy IpToEndPoint");
        }



        private void buttonMake_Click(object sender, EventArgs e)
        {
            newFile.XML_myIPAddress = textBox_Cloud_IP.Text;
            newFile.XML_myPortNumber = int.Parse(textBox_Cloud_Port.Text);

            configFilePath = textBox_path.Text;

            LoadingXMLFile.Serialization(configFilePath, newFile);

            MessageBox.Show("Dokonano serializacji.");
        }
    }
}
