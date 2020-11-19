using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OrdersBot
{
    class ButtonsTreatment
    {
        public Buttons buttonsTr = new Buttons();

        private static Dictionary<long, UserState> _clientState = new Dictionary<long, UserState>(); // List of user states. Список меню, в котором находится пользователь.

        Telegram.Bot.TelegramBotClient _client = new Telegram.Bot.TelegramBotClient("1473672623:AAErzl142jbrLSljTImnY8dJvhYRJgWcM64");

        public void processUpdate(Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:

                    var text = update.Message.Text;

                    string imagePath = null;

                    #region  Options for handling updates when a button is clicked. Варианты обработки обновлений в случае нажатия кнопки. 

                    switch (text)
                    {
                        case "/start":
                            _clientState[update.Message.Chat.Id] = new UserState { State = State.MainMenu };
                            var cl1 = _client.SendTextMessageAsync(update.Message.Chat.Id, "Пожалуйста, сделайте выбор:", replyMarkup: buttonsTr.MainMenuButtons()).Result;
                            break;

                        case "Перейти в меню заказов":
                            _clientState[update.Message.Chat.Id] = new UserState { State = State.MainMenu };
                            var cl2 = _client.SendTextMessageAsync(update.Message.Chat.Id, "Пожалуйста, выберите услугу: ", replyMarkup: buttonsTr.GetOrdersButtons()).Result;
                            break;

                        case "Вызов специалиста":
                            _clientState[update.Message.Chat.Id] = new UserState { State = State.MainMenu };
                            // TODO: call spec method.
                            break;

                        case "Заправка принтера":
                            imagePath = Path.Combine(Environment.CurrentDirectory, "price1.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var price1 = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на заправку принтера", replyMarkup: buttonsTr.GetInlineButton(1)).Result;
                            }
                            break;

                        case "Установка Windows":
                            imagePath = Path.Combine(Environment.CurrentDirectory, "price2.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var price2 = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на установку Windows", replyMarkup: buttonsTr.GetInlineButton(2)).Result;
                            }
                            break;

                        case "Монтаж локальной сети":
                            imagePath = Path.Combine(Environment.CurrentDirectory, "price3.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var price3 = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на монтаж локальных сетей", replyMarkup: buttonsTr.GetInlineButton(3)).Result;
                            }
                            break;

                        case "Ремонт компьютерной техники":
                            imagePath = Path.Combine(Environment.CurrentDirectory, "price4.png");
                            using (var stream = File.OpenRead(imagePath))
                            {
                                var price4 = _client.SendPhotoAsync(update.Message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream), caption: "Прайс-лист на ремонт принтера", replyMarkup: buttonsTr.GetInlineButton(4)).Result;
                            }
                            break;

                        case "Вернуться в предыдущее меню":
                            _clientState[update.Message.Chat.Id] = new UserState { State = State.MainMenu };
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Пожалуйста, сделайте выбор:", replyMarkup: buttonsTr.MainMenuButtons());
                            break;

                        default:
                            _client.SendTextMessageAsync(update.Message.Chat.Id, "Нет подходящих вариантов для: " + text, replyMarkup: buttonsTr.MainMenuButtons());
                            break;
                    }
                    break;

                #endregion

                #region Sending a response to the user if any button is processed. Отправка ответа пользователю в случае обработки какой-либо кнопки. 

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:

                    switch (update.CallbackQuery.Data)
                    {
                        case "1":
                            var msg1 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на заправку принтера принят.", replyMarkup: buttonsTr.GetOrdersButtons()).Result;
                            break;

                        case "2":
                            var msg2 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на установку Windows принят.", replyMarkup: buttonsTr.GetOrdersButtons()).Result;
                            break;

                        case "3":
                            var msg3 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на монтаж локальной сети принят.", replyMarkup: buttonsTr.GetOrdersButtons()).Result;
                            break;

                        case "4":
                            var msg4 = _client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Заказ на ремонт принтера принят.", replyMarkup: buttonsTr.GetOrdersButtons()).Result;
                            break;
                    }
                    break;

                #endregion

                default:
                    Console.WriteLine(update.Type + " Not implemented");
                    break;
            }
        }
    }
}
