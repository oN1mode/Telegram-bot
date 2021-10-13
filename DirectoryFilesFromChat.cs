using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class DirectoryFilesFromChat
    {
        static public string PathToDirectory { get; }


        static DirectoryFilesFromChat()
        {
            PathToDirectory = ReturnPathToDirectoryFiles();
        }

        private static string ReturnPathToDirectoryFiles()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\DirectoryFilesFromChat\";
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

        public static FileInfo [] ReturnListFilesToDirectoryFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathToDirectory);
            FileInfo[] filesDirectory = dirInfo.GetFiles();
            return filesDirectory; 
        }

        public static int QuantityFilesToDirectoryFiles()
        {
            return Directory.GetFiles(PathToDirectory).Length;
        }
    }
}
