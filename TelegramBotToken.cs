using System;
using System.IO;


namespace Module_9
{
    /// <summary>
    /// Класс считывающий токен для работы с telegram api
    /// </summary>
    class TelegramBotToken
    {
        public string Token { get; }

        public TelegramBotToken()
        {
            Token = AssigningTokenFromFile();
        }

        /// <summary>
        /// Метод считывания токена из файла расположенном на диске
        /// </summary>
        /// <returns></returns>
        private string AssigningTokenFromFile()
        {
            FileInfo file = new FileInfo(@"C:\token.txt");
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

        public override string ToString()
        {
            return $"{Token}";
        }

    }
}
