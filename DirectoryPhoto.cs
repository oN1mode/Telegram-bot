using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    public static class DirectoryPhoto
    {
        static public string PathToDirectory { get; }


        static DirectoryPhoto()
        {
            PathToDirectory = ReturnPathToDirectoryPhoto();
        }

        private static string ReturnPathToDirectoryPhoto ()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\PhotoTelegramBot\";
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

        public static List<string> ReturnListFilesToDirectoryPhoto()
        {
            List<string> nameFilesToDirectory = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\PhotoTelegramBot");
            nameFilesToDirectory.Add(dirInfo.GetFiles().ToString());
            return nameFilesToDirectory;
        }

        public static int QuantityFilesToDirectoryPhoto()
        {
            int quantityFilesToDirectiry;
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\PhotoTelegramBot");
            quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            return quantityFilesToDirectiry;
        }
    }
}
