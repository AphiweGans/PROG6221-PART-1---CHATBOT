using System;

namespace CyberAware
{
    public class Chatbot
    {
        private readonly ResponseHandler responseHandler = new();
        private readonly string userName;

        public Chatbot(string userName)
        {
            this.userName = userName;
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Hello {userName}, type 'help' to see available topics.");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nYou: ");
                Console.ResetColor();

                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Goodbye {userName}! Stay safe online.");
                    Console.ResetColor();
                    break;
                }

                string response = responseHandler.GetResponse(input, userName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Bot: {response}");
                Console.ResetColor();
            }
        }
    }
}
