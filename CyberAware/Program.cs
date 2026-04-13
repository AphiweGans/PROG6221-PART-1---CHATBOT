using System;

namespace CyberAware
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter your name: ");
            Console.ResetColor();
            string userName = Console.ReadLine() ?? "User";

            GreetingManager greetingManager = new GreetingManager();
            greetingManager.ShowGreeting();

            Chatbot chatbot = new Chatbot(userName);
            chatbot.Start();
        }
    }
}
