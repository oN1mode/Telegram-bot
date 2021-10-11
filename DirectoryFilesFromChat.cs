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

        public static string[] ReturnListFilesToDirectoryFiles()
        {
            //DirectoryInfo dirInfo = new DirectoryInfo(PathToDirectory);
            return Directory.GetFiles(PathToDirectory); //dirInfo.ToString()
        }

        public static int QuantityFilesToDirectoryFiles()
        {
            //int quantityFilesToDirectiry;
            //DirectoryInfo dirInfo = new DirectoryInfo(PathToDirectory);
            //quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            //return quantityFilesToDirectiry;
            return Directory.GetFiles(PathToDirectory).Length;
        }
    }
}
