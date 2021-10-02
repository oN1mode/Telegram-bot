using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using System.IO;
using System.Net;
using Telegram.Bot.Args;

namespace Module_9
{
    class Program
    {
        private readonly static string token = File.ReadAllText(@"C:\token.txt");
        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TelegramBotToken tokenBot = new TelegramBotToken();
            bot = new TelegramBotClient(tokenBot.ToString());
            bot.OnMessage += MessageList;
            bot.StartReceiving();
            Console.ReadKey();
            bot.StopReceiving();
        }

        private static void MessageList(object sender, MessageEventArgs msg)
        {



            throw new NotImplementedException();
        }
    }
}
