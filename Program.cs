using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using System.IO;
using System.Net;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

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
            if (tokenBot.Token == null) return;
            bot = new TelegramBotClient(tokenBot.Token);
            bot.OnMessage += MessageList;
            bot.StartReceiving();
            Console.ReadKey();
            bot.StopReceiving();
        }

        private static async void MessageList(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Type == MessageType.Photo) await DownloadPhotoFromChat(msg);
            if (msg.Type == MessageType.Document) await DownloadFileFromChat(msg);
            if (msg.Type == MessageType.Video) await DownloadVideoFromChat(msg);
            if (msg.Type == MessageType.Audio) await DownloadAudioFromChat(msg);
            if (msg.Type == MessageType.Text) await TextMessageHandler(msg);
            if (msg.Type == MessageType.Voice) await DownloadVoiceFromChat(msg);
            
            await bot.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
            

        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "Привет"}, new KeyboardButton { Text = "Список файлов на диске"}
                        
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "Курс валют на сегодня"}, new KeyboardButton { Text = "Пока"}
                    }
                }
            };

        }

        private static async Task TextMessageHandler(Telegram.Bot.Types.Message msg)
        {
            if (msg.Text != null)
            {
                Console.WriteLine($"пришло сообщение: {msg.Text}");

                if (msg.Text.ToLower() == "hi" || msg.Text.ToLower() == "hello" || msg.Text.ToLower() == "привет" || msg.Text.ToLower() == "здорово" || msg.Text.ToLower() == "здарово" || msg.Text.ToLower() == "хай")
                {
                    var stic = await bot.SendStickerAsync(msg.Chat.Id, sticker: "https://tlgrm.ru/_/stickers/f80/4f2/f804f23c-2691-332d-92e2-78bff6b9d47e/192/28.webp", replyToMessageId: msg.MessageId);
                }
                switch (msg.Text)
                {
                    case "Привет":
                        break;

                    default:
                        await bot.SendTextMessageAsync(msg.Chat.Id, "Выберите команду: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static async Task DownloadFileFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Document.FileId);
            using (FileStream fs = new FileStream(DirectoryFiles.PathToDirectory + msg.Document.FileName, FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadPhotoFromChat(Telegram.Bot.Types.Message msg)
        {
            
            var file = await bot.GetFileAsync(msg.Photo[msg.Photo.Length - 1].FileId);
            using (FileStream fs = new FileStream(DirectoryPhoto.PathToDirectory + ("photo_" + DirectoryPhoto.QuantityFilesToDirectoryPhoto() + ".jpg"), FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
            
        }

        private static async Task DownloadVideoFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Video.FileId);
            using (FileStream fs = new FileStream(DirectoryVideo.PathToDirectory + ("video_" + DirectoryVideo.QuantityFilesToDirectoryVideo() + ".mp4"), FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadAudioFromChat (Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Audio.FileId);
            using (FileStream fs = new FileStream(DirectoryAudio.PathToDirectory + msg.Audio.Title + ".mp3", FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadVoiceFromChat (Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Voice.FileId);
            using (FileStream fs = new FileStream(DirectoryVoice.PathToDirectory + ("voice_" + DirectoryVoice.QuantityFilesToDirectoryVoice() + ".ogg") ,FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }
    }
}
