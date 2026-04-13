using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CyberAware
{
    public class AudioPlayer
    {
        public void Play(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Logger.Warn($"Audio file not found at '{path}'. Skipping voice greeting. Place your welcome.wav in assets/welcome.wav to enable audio.");
                    return;
                }

                Logger.Info($"Attempting to play audio: {path}");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    try
                    {
                        var player = new System.Media.SoundPlayer(path);
                        player.Play();
                        Logger.Info("Played audio using System.Media.SoundPlayer.");
                        return;
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"SoundPlayer failed: {ex.Message}");
                    }
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (TryRunProcess("aplay", $"\"{path}\""))
                    {
                        Logger.Info("Played audio using aplay.");
                        return;
                    }
                    if (TryRunProcess("ffplay", $"-nodisp -autoexit \"{path}\""))
                    {
                        Logger.Info("Played audio using ffplay.");
                        return;
                    }
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (TryRunProcess("afplay", $"\"{path}\""))
                    {
                        Logger.Info("Played audio using afplay.");
                        return;
                    }
                }

                Logger.Warn("No supported audio player was available on this platform. Skipping audio playback.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error playing audio: {ex.Message}");
            }
        }

        private static bool TryRunProcess(string fileName, string arguments)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
