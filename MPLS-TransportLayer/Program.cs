using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Aplikacja CHMURA KABLOWA
 * - odpowiada wszystkim łączom wystepującym w sieci
 * 
 * - osoba odpowiedzialna: Krzysztof Kossowski 
*/

namespace MPLS_TransportLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            PortsClass nowy = new PortsClass();
            string end = null;

            do
            {
                Console.WriteLine("...");
                end = Console.ReadLine();
            }
            while (end != "end");
        }
    }
}
