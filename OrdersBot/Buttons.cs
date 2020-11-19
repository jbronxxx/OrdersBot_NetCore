using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace OrdersBot
{
    public class Buttons
    {
        #region Constants representing buttons. Константы, представляющие кнопки

        private const string BUTTON_START = "/start"; // Start button.

        private const string BUTTON_ORDERS_MENU = "Перейти в меню заказов"; // Orders menu button.

        private const string BUTTON_CALL_SPEC = "Вызов специалиста"; // Calling a specialist

        private const string BUTTON_1 = "Заправка принтера";  // Refill printer.

        private const string BUTTON_2 = "Установка Windows";  // Install Windows operating system.

        private const string BUTTON_3 = "Монтаж локальной сети";  // Local network installation.

        private const string BUTTON_4 = "Ремонт компьютерной техники";  // Repair of computer equipment.

        private const string BUTTON_BACK = "Вернуться в предыдущее меню";  // Return to previous menu. 

        #endregion

        #region Button embedded in the chat field. Кнопка, встроенная в поле переписки. 

        public IReplyMarkup GetInlineButton(int id)
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton { Text = "Заказать", CallbackData = id.ToString() });
        }

        #endregion

        #region Keyboard with a selection of ready-made buttons. Клавиатура с выбором готовых кнопок. 

        public IReplyMarkup MainMenuButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = BUTTON_ORDERS_MENU }, },
                    new List<KeyboardButton>{new KeyboardButton {Text = BUTTON_CALL_SPEC }, }
                },

                // Resize the keyboard. Изменение размера клавиатуры. 
                ResizeKeyboard = true
            };
        }
        public IReplyMarkup GetOrdersButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
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
