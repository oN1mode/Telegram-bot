using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class DirectoryFiles
    {
        static public string PathToDirectory { get; }


        static DirectoryFiles()
        {
            PathToDirectory = ReturnPathToDirectoryFiles();
        }

        private static string ReturnPathToDirectoryFiles()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\FilesTelegramBot\";
            DirectoryInfo dirInfo = new DirectoryInfo(pathDirectory);
            if (dirInfo.Exists)
            {
                path = dirInfo.FullName;
                return path;
            }
            else
            {
                Directory.CreateDirectory(dirInfo.FullName);
                path = dirInfo.FullName;
                return path;
            }
        }

        public static List<string> ReturnListFilesToDirectoryFiles()
        {
            List<string> nameFilesToDirectory = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\FilesTelegramBot");
            nameFilesToDirectory.Add(dirInfo.GetFiles().ToString());
            return nameFilesToDirectory;
        }

        public static int QuantityFilesToDirectoryFiles()
        {
            int quantityFilesToDirectiry;
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\FilesTelegramBot");
            quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            return quantityFilesToDirectiry;
        }
    }
}
