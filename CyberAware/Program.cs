using System;

namespace CyberAware
{
    class Program
    {
        static void Main(string[] args)
        {
            GreetingManager greetingManager = new GreetingManager();
            // Show greeting (includes playing audio) before asking for the user's name
            greetingManager.ShowGreeting();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter your name: ");
            Console.ResetColor();
            string userName = Console.ReadLine() ?? "User";

            Chatbot chatbot = new Chatbot(userName);
            chatbot.Start();
        }
    }
}
