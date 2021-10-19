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
            FileInfo file = new FileInfo("token.txt");
            string token = null;
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                        token = sr.ReadToEnd();
                }
                if (String.IsNullOrEmpty(token))
                {
                    Console.WriteLine($"В файле {file.FullName} отсутствует token!");
                    string inputToken = InputTokenToConsole();
                    WriteInputTokenToFile(inputToken);
                    token = AssigningTokenFromFile();
                }
                return token;
            }
            else
            {
                string inputToken = InputTokenToConsole();
                CreatedFileForToken();
                WriteInputTokenToFile(inputToken);
                token = AssigningTokenFromFile();
                return token;
            }

        }

        /// <summary>
        /// Метод для записи введеного пользователем токена в файл
        /// </summary>
        /// <param name="inputToken">введенный токен</param>
        private static void WriteInputTokenToFile(string inputToken)
        {
            File.WriteAllText("token.txt", inputToken);
        }

        /// <summary>
        /// Метод создания файла для хранения токена
        /// </summary>
        private static void CreatedFileForToken()
        {
            File.Create("token.txt");
        }

        /// <summary>
        /// Метод для обработки введенного токена в консоль
        /// </summary>
        /// <returns></returns>
        private static string InputTokenToConsole()
        {
            Console.Write("Для работоспособности программы Вам необходимо ввести токен: ");
            string inputToken = Console.ReadLine();
            inputToken.Trim();
            if (String.IsNullOrEmpty(inputToken))
            {
                Console.WriteLine("Вы ввели пустую строку, попробуйте ещё раз");
                inputToken = InputTokenToConsole();
            }
            return inputToken;
        }

        public override string ToString()
        {
            return $"{Token}";
        }

    }
}
