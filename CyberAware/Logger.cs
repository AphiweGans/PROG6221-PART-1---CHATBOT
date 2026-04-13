using System;
using System.IO;
using System.Threading;

namespace CyberAware
{
    internal static class Logger
    {
        private static readonly object FileLock = new();
        private static readonly string LogFilePath;

        static Logger()
        {
            try
            {
                LogFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "assets", "log.txt");
                var dir = Path.GetDirectoryName(LogFilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch
            {
                // ignore errors - logging to file is best-effort
                LogFilePath = null!;
            }
        }

        // Write INFO messages to the log file only (no green console output)
        public static void Info(string message) => WriteToFileOnly("INFO", message);
        public static void Warn(string message) => Write(ConsoleColor.DarkYellow, "WARN", message);
        public static void Error(string message) => Write(ConsoleColor.DarkRed, "ERROR", message);

        private static void Write(ConsoleColor color, string level, string message)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            var timestamp = DateTime.UtcNow.ToString("o");
            var line = $"[{timestamp}] [{level}] {message}";
            Console.WriteLine(line);
            Console.ForegroundColor = prev;

            if (string.IsNullOrEmpty(LogFilePath))
                return;

            try
            {
                lock (FileLock)
                {
                    File.AppendAllText(LogFilePath, line + Environment.NewLine);
                }
            }
            catch
            {
                // ignore file logging failures
            }
        }

        private static void WriteToFileOnly(string level, string message)
        {
            if (string.IsNullOrEmpty(LogFilePath))
                return;

            try
            {
                var timestamp = DateTime.UtcNow.ToString("o");
                var line = $"[{timestamp}] [{level}] {message}";
                lock (FileLock)
                {
                    File.AppendAllText(LogFilePath, line + Environment.NewLine);
                }
            }
            catch
            {
                // ignore file logging failures
            }
        }
    }
}