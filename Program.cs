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
using Telegram.Bot.Types.InputFiles;

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
        }


        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "/start"}, new KeyboardButton { Text = "/help"}

                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "Курс валют на сегодня"}, new KeyboardButton { Text = "Список файлов на диске"}
                    }
                }
            };

        }

        private static async Task TextMessageHandler(Telegram.Bot.Types.Message msg)
        {
            if (msg.Text != null)
            {
                Console.WriteLine($"пришло сообщение: {msg.Text}");
                if (msg.Text == "/start")
                {
                    await bot.SendTextMessageAsync(msg.Chat.Id, "Добро пожаловать! Меня зовут Lazy_bot. Я умею немного общаться, а так же могу предоставлять актуальный курс по валютам USD и EUR");
                }
                else if (msg.Text.ToLower() == "hi" || msg.Text.ToLower() == "hello" || msg.Text.ToLower() == "привет" || msg.Text.ToLower() == "здорово" || msg.Text.ToLower() == "здарово" || msg.Text.ToLower() == "хай")
                {
                    var stic = await bot.SendStickerAsync(msg.Chat.Id, sticker: "https://tlgrm.ru/_/stickers/f80/4f2/f804f23c-2691-332d-92e2-78bff6b9d47e/192/28.webp", replyToMessageId: msg.MessageId);
                }
                else if (msg.Text == "/help")
                {
                    await bot.SendTextMessageAsync(msg.Chat.Id, "Доступные команды представлены ниже, выбери одну из них", replyMarkup: GetButtons());
                }
                else if (msg.Text.ToLower() == "список файлов на диске" || msg.Text.ToLower() == "/список файлов на диске")
                {
                    await SendNamesFilesToChatFromDirectoryFilesFromChat(msg);

                }
            }
        }

        private static async Task SendNamesFilesToChatFromDirectoryFilesFromChat(Telegram.Bot.Types.Message msg)
        {
            FileInfo[] titleFilesToDirectory = DirectoryFilesFromChat.ReturnListFilesToDirectoryFiles();
            int count = default(int);
            foreach (var title in titleFilesToDirectory)
            {
                string messageToChat = String.Format($"{(++count)}" + ". " + $"{title.Name}");
                await bot.SendTextMessageAsync(msg.Chat.Id, messageToChat);
            }

            await CreatedKeyboradButton(msg, count);
        }

        private static async Task CreatedKeyboradButton(Telegram.Bot.Types.Message msg, int count)
        {
            var keyboard = new List<List<InlineKeyboardButton>>();
            var cols = new List<InlineKeyboardButton>();

            for (int i = 1; i <= count; i++)
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(i.ToString(), i.ToString()));
                if (i == count) keyboard.Add(cols);
                if (i % 3 != 0) continue;
                keyboard.Add(cols);
                cols = new List<InlineKeyboardButton>();
            }

            var replyMarkup = new InlineKeyboardMarkup(keyboard);
            await SendKeyboardButtonForChat(msg, replyMarkup);
        }

        private static async Task SendKeyboardButtonForChat(Telegram.Bot.Types.Message msg, InlineKeyboardMarkup replyMarkup)
        {
            await bot.SendTextMessageAsync(msg.Chat.Id, "Выберите вариант", replyMarkup: replyMarkup);
            HandlerCallbackQuery();
        }

        private static void HandlerCallbackQuery()
        {
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs eventArgs) =>
            {
                var msg = eventArgs.CallbackQuery.Message;
                int count = DirectoryFilesFromChat.QuantityFilesToDirectoryFiles();
                FileInfo[] filesFromDirectory = DirectoryFilesFromChat.ReturnListFilesToDirectoryFiles();

                for (int i = 1; i <= count; i++)
                {
                    if (eventArgs.CallbackQuery.Data == i.ToString())
                    {
                        string pathFile = filesFromDirectory[i - 1].FullName;
                        string nameFile = filesFromDirectory[i - 1].Name;
                        string extencionFile = filesFromDirectory[i - 1].Extension;
                        if (extencionFile == ".jpg") await SendPhotoToChat(msg, pathFile, nameFile);
                        else if (extencionFile == ".mp4") await SendVideoToChat(msg, pathFile, nameFile);
                        else if (extencionFile == ".mp3") await SendAudioToChat(msg, pathFile, nameFile);
                        else if (extencionFile == ".ogg") await SendVoiceToChat(msg, pathFile, nameFile);
                        else await SendDocumentToChat(msg, pathFile, nameFile);
                    }
                }
            };
        }

        #region Методы для отправки в чат файлов, расположенных на диске
        private static async Task SendPhotoToChat(Telegram.Bot.Types.Message msg, string pathFile, string nameFile)
        {
            using (FileStream fs = File.OpenRead(pathFile))
            {
                InputOnlineFile inputOnlineFiles = new InputOnlineFile(fs, nameFile);
                await bot.SendPhotoAsync(msg.Chat.Id, fs);
            }
        }

        private static async Task SendVideoToChat(Telegram.Bot.Types.Message msg, string pathFile, string nameFile)
        {
            using (FileStream fs = File.OpenRead(pathFile))
            {
                InputOnlineFile inputOnlineFiles = new InputOnlineFile(fs, nameFile);
                await bot.SendVideoAsync(msg.Chat.Id, fs);
            }
        }

        private static async Task SendAudioToChat(Telegram.Bot.Types.Message msg, string pathFile, string nameFile)
        {
            using (FileStream fs = File.OpenRead(pathFile))
            {
                InputOnlineFile inputOnlineFiles = new InputOnlineFile(fs, nameFile);
                await bot.SendAudioAsync(msg.Chat.Id, fs);
            }
        }

        private static async Task SendVoiceToChat(Telegram.Bot.Types.Message msg, string pathFile, string nameFile)
        {
            using (FileStream fs = File.OpenRead(pathFile))
            {
                InputOnlineFile inputOnlineFiles = new InputOnlineFile(fs, nameFile);
                await bot.SendVoiceAsync(msg.Chat.Id, fs);
            }
        }

        private static async Task SendDocumentToChat(Telegram.Bot.Types.Message msg, string pathFile, string nameFile)
        {
            using (FileStream fs = File.OpenRead(pathFile))
            {
                InputOnlineFile inputOnlineFiles = new InputOnlineFile(fs, nameFile);
                await bot.SendDocumentAsync(msg.Chat.Id, fs);
            }
        }
        #endregion

        #region Методы для загрузки из чата файлов, присланных пользователями

        private static async Task DownloadFileFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Document.FileId);
            using (FileStream fs = new FileStream(DirectoryFilesFromChat.PathToDirectory + msg.Document.FileName, FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadPhotoFromChat(Telegram.Bot.Types.Message msg)
        {

            var file = await bot.GetFileAsync(msg.Photo[msg.Photo.Length - 1].FileId);
            using (FileStream fs = new FileStream(DirectoryFilesFromChat.PathToDirectory + ("photo_" + DirectoryFilesFromChat.QuantityFilesToDirectoryFiles() + ".jpg"), FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }

        }

        private static async Task DownloadVideoFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Video.FileId);
            using (FileStream fs = new FileStream(DirectoryFilesFromChat.PathToDirectory + ("video_" + DirectoryFilesFromChat.QuantityFilesToDirectoryFiles() + ".mp4"), FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadAudioFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Audio.FileId);
            using (FileStream fs = new FileStream(DirectoryFilesFromChat.PathToDirectory + msg.Audio.Title + ".mp3", FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static async Task DownloadVoiceFromChat(Telegram.Bot.Types.Message msg)
        {
            var file = await bot.GetFileAsync(msg.Voice.FileId);
            using (FileStream fs = new FileStream(DirectoryFilesFromChat.PathToDirectory + ("voice_" + DirectoryFilesFromChat.QuantityFilesToDirectoryFiles() + ".ogg"), FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }
    
        #endregion
    }
}
