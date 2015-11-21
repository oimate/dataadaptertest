using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace bccxor
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            string read = @"6C$50$25$43$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$80$D3$C1$CA$CD$CF$CE$80$80$80$80$80$10$03";
            string[] split = read.Split('$');

            int xor = int.Parse(split[0], System.Globalization.NumberStyles.HexNumber);
            for (int i = 1; i < split.Length; i++)
            {
                xor ^= int.Parse(split[i], System.Globalization.NumberStyles.HexNumber);
            }

            string outstr = string.Format("{0:X2}", xor);
            Console.WriteLine(outstr);
            Clipboard.SetText(outstr);
            Console.Read();
        }
    }
}
