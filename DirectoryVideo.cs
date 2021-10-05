using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class DirectoryVideo
    {
        static public string PathToDirectory { get; }


        static DirectoryVideo()
        {
            PathToDirectory = ReturnPathToDirectoryVideo();
        }

        private static string ReturnPathToDirectoryVideo()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\VideoTelegramBot\";
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

        public static List<string> ReturnListFilesToDirectoryVideo()
        {
            List<string> nameFilesToDirectory = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\VideoTelegramBot");
            nameFilesToDirectory.Add(dirInfo.GetFiles().ToString());
            return nameFilesToDirectory;
        }

        public static int QuantityFilesToDirectoryVideo()
        {
            int quantityFilesToDirectiry;
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\VideoTelegramBot");
            quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            return quantityFilesToDirectiry;
        }
    }
}
