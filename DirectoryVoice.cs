using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class DirectoryVoice
    {
        static public string PathToDirectory { get; }


        static DirectoryVoice()
        {
            PathToDirectory = ReturnPathToDirectoryVoice();
        }

        private static string ReturnPathToDirectoryVoice()
        {
            string path;
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\VoiceTelegramBot\";
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

        public static List<string> ReturnListFilesToDirectoryVoice()
        {
            List<string> nameFilesToDirectory = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\VoiceTelegramBot");
            nameFilesToDirectory.Add(dirInfo.GetFiles().ToString());
            return nameFilesToDirectory;
        }

        public static int QuantityFilesToDirectoryVoice()
        {
            int quantityFilesToDirectiry;
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\VoiceTelegramBot");
            quantityFilesToDirectiry = dirInfo.GetFiles().Length;
            return quantityFilesToDirectiry;
        }
    }
}
}
