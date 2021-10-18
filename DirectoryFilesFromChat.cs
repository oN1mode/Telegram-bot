using System;
using System.IO;

namespace Module_9
{
    /// <summary>
    /// Класс для работы с директорией, в которой хранятся файлы из чата
    /// </summary>
    class DirectoryFilesFromChat
    {
        static public string PathToDirectory { get; }


        static DirectoryFilesFromChat()
        {
            PathToDirectory = ReturnPathToDirectoryFiles();
        }

        /// <summary>
        /// Метод для указания пути к директории где расположены сохраненные файлы из чата
        /// </summary>
        /// <returns>возвращает путь к файлу</returns>
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

        /// <summary>
        /// Метод для 
        /// </summary>
        /// <returns>возвращает список файлов хранящихся в директории</returns>
        public static FileInfo [] ReturnListFilesToDirectoryFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathToDirectory);
            FileInfo[] filesDirectory = dirInfo.GetFiles();
            return filesDirectory; 
        }

        /// <summary>
        /// Метод для выяснения кол-ва файлов расположенных в директории
        /// </summary>
        /// <returns>возвращает кол-ва файлов расположенных в директории</returns>
        public static int QuantityFilesToDirectoryFiles()
        {
            return Directory.GetFiles(PathToDirectory).Length;
        }
    }
}
