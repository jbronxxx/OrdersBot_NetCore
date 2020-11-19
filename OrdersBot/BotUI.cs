using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class BotUI
    {
        #region Constants representing buttons. Константы, представляющие кнопки

        private const string BUTTON_START = "Запустить бота"; // Start the bot.

        private const string BUTTON_1 = "Заправка принтера";  // Refill printer.

        private const string BUTTON_2 = "Установка Windows";  // Install Windows operating system.

        private const string BUTTON_3 = "Монтаж локальной сети";  // Local network installation.

        private const string BUTTON_4 = "Ремонт компьютерной техники";  // Repair of computer equipment.

        private const string BUTTON_BACK = "Вернуться в предыдущее меню";  // Return to previous menu. 

        #endregion

        private string _token;

        Telegram.Bot.TelegramBotClient _client;

        public BotUI(string token)
        {
            this._token = token;
        }

        #region  Method that polls the Telegram server for updates with an interval Thread.Sleep (1000). Метод, опрашивающий сервер Телеграм на наличие обновлений с интервалом Thread.Sleep(1000).

        internal void GetUpdates()
        {
            _client = new Telegram.Bot.TelegramBotClient(_token);

            var me = _client.GetMeAsync().Result;

            if (me != null && !string.IsNullOrEmpty(me.Username))
            {
                int offset = 0;

                while (true)
                {
                    try
                    {
                        var updates = _client.GetUpdatesAsync(offset).Result;
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                processUpdate(update);
                                offset = update.Id + 1;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Thread.Sleep(1000);
                }
            }
        }

        #endregion

        #region Update processing method. Метод обработки обновлений. 

        private void processUpdate(Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:

                    var text = update.Message.Text;

                    string imagePath = null;

                    #region  Options for handling updates when a button is clicked. Варианты обработки обновлений в случае нажатия кнопки. 

                    switch (text)
                    {
                        case BUTTON_1:
                            imagePath = Path.Combine(Environment.CurrentDirectory, "1.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var r = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на заправку принтера", replyMarkup: GetInlineButton(1)).Result;
                            }
                            break;

                        case BUTTON_2:
                            imagePath = Path.Combine(Environment.CurrentDirectory, "2.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var r = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на установку Windows", replyMarkup: GetInlineButton(2)).Result;
                            }
                            break;

                        case BUTTON_3:
                            imagePath = Path.Combine(Environment.CurrentDirectory, "3.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var r = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на монтаж локальных сетей", replyMarkup: GetInlineButton(3)).Result;
                            }
                            break;

                        case BUTTON_4:
                            imagePath = Path.Combine(Environment.CurrentDirectory, "4.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var r = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на ремонт принтера", replyMarkup: GetInlineButton(4)).Result;
                            }
                            break;

                        case BUTTON_START:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Бот запущен. Пожалуйста, выберите команду: ", replyMarkup: GetButtons());
                            break;

                        default:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Нет подходящих вариантов для: " + text, replyMarkup: GetButtons());
                            break;
                    }
                    break;

                #endregion

                #region Sending a response to the user if any button is processed. Отправка ответа пользователю в случае обработки какой-либо кнопки. 

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:

                    switch (update.CallbackQuery.Data)
                    {
                        case "1":
                            var msg1 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на заправку принтера принят.", replyMarkup: GetButtons()).Result;
                            break;

                        case "2":
                            var msg2 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на установку Windows принят.", replyMarkup: GetButtons()).Result;
                            break;

                        case "3":
                            var msg3 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на монтаж локальной сети принят.", replyMarkup: GetButtons()).Result;
                            break;

                        case "4":
                            var msg4 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на ремонт принтера принят.", replyMarkup: GetButtons()).Result;
                            break;
                    }
                    break;

                #endregion

                default:
                    Console.WriteLine(update.Type + " Not implemented");
                    break;
            }
        }

        #endregion

        #region Button embedded in the chat field. Кнопка, встроенная в поле переписки. 

        private IReplyMarkup GetInlineButton(int id)
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton { Text = "Заказать", CallbackData = id.ToString() });
        }

        #endregion

        #region Keyboard with a selection of ready-made buttons. Клавиатура с выбором готовых кнопок. 

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = BUTTON_START }, },
                    new List<KeyboardButton>{new KeyboardButton {Text = BUTTON_1 }, new KeyboardButton {Text = BUTTON_2 }, },
                    new List<KeyboardButton>{new KeyboardButton {Text = BUTTON_3 }, new KeyboardButton {Text = BUTTON_4 }, },
                    new List<KeyboardButton>{new KeyboardButton {Text = BUTTON_BACK }, },
                },

                // Resize the keyboard. Изменение размера клавиатуры. 
                ResizeKeyboard = true
            };
        }

        #endregion
    }
}