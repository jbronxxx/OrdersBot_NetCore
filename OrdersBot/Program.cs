
using System;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            try
            {
                BotUI hlp = new BotUI(token: "1473672623:AAErzl142jbrLSljTImnY8dJvhYRJgWcM64");

                hlp.GetUpdates();
                {

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}