using System;
using System.IO;

namespace CyberAware
{
    public class GreetingManager
    {
        private readonly AudioPlayer audioPlayer = new();

        public void ShowGreeting()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=========================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
   ____       _                ____        _   
  / ___|  ___| |__   ___ _ __ | __ )  ___ | |_ 
  \___ \ / __| '_ \ / _ \ '_ \|  _ \ / _ \| __|
   ___) | (__| | | |  __/ | | | |_) | (_) | |_ 
  |____/ \___|_| |_|\___|_| |_|____/ \___/ \__|
        Cybersecurity Awareness Bot
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=========================================");
            Console.ResetColor();

            string audioPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "assets", "greeting.wav");
            audioPlayer.Play(audioPath);
        }
    }
}
