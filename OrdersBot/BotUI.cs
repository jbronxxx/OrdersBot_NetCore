using System;
using System.Linq;
using System.Threading;

namespace OrdersBot
{
    internal class BotUI
    {
        public ButtonsTreatment buttons = new ButtonsTreatment();

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
                                buttons.processUpdate(update);
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
    }
}