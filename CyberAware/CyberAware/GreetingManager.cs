using System;
using System.IO;

namespace CyberAware
{
    public class GreetingManager
    {
        private readonly AudioPlayer audioPlayer = new();

        public void ShowGreeting()
        {
            // Resolve path to the assets folder inside the app's output directory.
            // Ensure the greeting.wav is copied to output (see csproj change) and then load from there.
            string audioPath = Path.Combine(AppContext.BaseDirectory, "assets", "greeting.wav");
            // Play voice greeting first, then show ASCII art banner
            audioPlayer.Play(audioPath);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
    ██████╗██╗   ██╗██████╗ ███████╗██████╗  █████╗ ██╗    ██╗ █████╗ ██████╗ ███████╗
  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██║    ██║██╔══██╗██╔══██╗██╔════╝
  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████║██║ █╗ ██║███████║██████╔╝█████╗  
  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██║██║███╗██║██╔══██║██╔══██╗██╔══╝  
  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██║  ██║╚███╔███╔╝██║  ██║██║  ██║███████╗
   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝
                                                                                   
        ░▒▓█ SHADOW LAYER  █▓▒░
         ░▒▓████████████████████████▓▒░
            ░▒▓ CYBERAWARE ▓▒░

        Awareness is Your Firewall. Protect What Matters. Stay Alert. Stay Protected.
");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================================================================================");
            Console.ResetColor();


        }
    }
}
