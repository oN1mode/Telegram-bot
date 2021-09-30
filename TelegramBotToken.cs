using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_9
{
    class TelegramBotToken
    {
        private string token;
        public string Token { get; }

        public TelegramBotToken()
        {
            this.token = AssigningTokenFromFile();
        }

        private string AssigningTokenFromFile()
        {
            FileInfo file = new FileInfo("token.txt");
            string token = null;
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                        token = sr.ReadToEnd();
                }
                if (token == null)
                {
                    Console.WriteLine($"В файле {file.FullName} отсутствует token!");
                    Console.ReadKey();
                }
                return token;
            }
            else
            {
                Console.WriteLine($"Файл token.txt не сущществует, пожалуйста создайте его и запишите в него token");
                return token;
            }
        }

    }
}
