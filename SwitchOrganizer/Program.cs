using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir;
            Console.Write("Inserire Directory: ");
            dir = Console.ReadLine();

            // Check if dirs exist
            if (!Directory.Exists(dir + @"\BASE"))
            {
                Directory.CreateDirectory(dir + @"\BASE");
            }

            if (!Directory.Exists(dir + @"\UPDATE"))
            {
                Directory.CreateDirectory(dir + @"\UPDATE");
            }

            if (!Directory.Exists(dir + @"\DLC"))
            {
                Directory.CreateDirectory(dir + @"\DLC");
            }

            string[] file = Directory.GetFiles(dir, "*.ns?", SearchOption.AllDirectories);
            foreach (string a in file)
            {
                string[] fileparts = a.Split('[');
                // Ricerca di elementi
                foreach (string b in fileparts)
                {
                    // se contiene una v
                    if (b.Contains("v"))
                    {
                        string[] tmp = b.Split('.');
                        string versionstring = tmp[0].TrimStart('v');
                        versionstring = versionstring.TrimEnd(']');

                        if (int.TryParse(versionstring, out int version))
                        {
                            if (version == 0)
                            {
                                File.Move(a, dir + @"\BASE\" + Path.GetFileName(a));
                            }
                            else
                            {
                                File.Move(a, dir + @"\UPDATE\" + Path.GetFileName(a));
                            }
                            break;
                        }
                    }
                    // se è dlc
                    if (b.Contains("DLC"))
                    {
                        File.Move(a, dir + @"\DLC\" + Path.GetFileName(a));
                        break;
                    }
                }
            }
            //Console.ReadLine();
        }
    }
}
