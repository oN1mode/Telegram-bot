using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class DirectoryAudio
    {
        static public string PathToDirectory { get; }


        static DirectoryAudio()
        {
            PathToDirectory = ReturnPathToDirectoryAudio();
        }

        private static string ReturnPathToDirectoryAudio()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\AudioTelegramBot\";
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

        public static List<string> ReturnListFilesToDirectoryAudio()
        {
            List<string> nameFilesToDirectory = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\AudioTelegramBot");
            nameFilesToDirectory.Add(dirInfo.GetFiles().ToString());
            return nameFilesToDirectory;
        }

        public static int QuantityFilesToDirectoryAudio()
        {
            int quantityFilesToDirectiry;
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\AudioTelegramBot");
            quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            return quantityFilesToDirectiry;
        }
    }
}
