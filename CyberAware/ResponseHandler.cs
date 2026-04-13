using System;
using System.Collections.Generic;

namespace CyberAware
{
    public class ResponseHandler
    {
        private readonly Dictionary<string, string> topics = new()
        {
            { "password", "Strong passwords are your first line of defense. Use a long, unique passphrase for each account, combine upper and lower case letters, numbers and symbols, and avoid dictionary words or obvious substitutions. Consider using a reputable password manager to generate and store complex passwords safely, and enable two-factor authentication where available to add an extra layer of protection." },
            { "phishing", "Phishing is a cyberattack where criminals trick you into revealing sensitive information by impersonating trusted organizations. Phishing can arrive by email, text, or spoofed websites. Look for signs like unexpected requests for credentials, poor spelling or grammar, mismatched URLs, and urgent language. Always verify senders, avoid clicking links in unsolicited messages, and use multi-factor authentication to limit the damage of credential theft." },
            { "safe browsing", "Safe browsing means being cautious online to reduce exposure to threats. Use up-to-date browsers, keep extensions to a minimum, and avoid downloading files from unknown sources. Check site certificates on sensitive pages, prefer HTTPS, and be careful when entering personal information. Use privacy settings, ad-blockers if appropriate, and enable security features like site isolation to reduce risk." },
            { "malware", "Malware is malicious software designed to damage, steal, or control systems. It includes viruses, worms, trojans, ransomware, spyware, and more. Prevent malware by keeping software updated, running reputable antivirus or endpoint protection, avoiding dubious downloads and attachments, and applying the principle of least privilege. Back up important data regularly to recover from attacks like ransomware." },
            { "identity theft", "Identity theft occurs when criminals steal personal information to impersonate you, access accounts, or commit fraud. Protect yourself by monitoring credit reports, using strong unique passwords, enabling two-factor authentication, shredding sensitive documents, and being cautious about sharing personal data online. If you suspect theft, contact banks and credit agencies immediately and consider a fraud alert or freeze on your credit." },
            { "social media", "Social media can expose you to risks if you overshare personal information, accept unknown friend requests, or click on suspicious links. Adjust privacy settings to limit who can see your posts, avoid posting sensitive details like vacation dates or addresses, and be mindful of phishing attempts that come through messages. Use strong account security and review connected apps regularly." },
            { "two factor", "Two-factor authentication (2FA) adds an extra layer of security by requiring a second verification step beyond a password, such as a code from an authenticator app or a hardware token. 2FA greatly reduces the risk of account takeover even if passwords are compromised. Prefer authenticator apps or hardware keys over SMS where possible, because SMS can be intercepted." },
            { "updates", "Software updates are critical for security because they often include patches for vulnerabilities that attackers can exploit. Keep your operating system, applications, and firmware up to date. Enable automatic updates when practical, and test updates in critical environments to avoid disruptions. Regular patching is one of the most effective ways to reduce risk." },
            { "wifi", "Public Wi-Fi networks are convenient but often insecure; attackers may intercept traffic on open hotspots. Avoid accessing sensitive sites on public Wi-Fi, or use a trusted VPN to encrypt your connection. Ensure your home Wi-Fi uses a strong WPA2/WPA3 password and consider hiding the SSID or using a guest network for visitors." },
            { "virus", "A computer virus is a type of malware that attaches to legitimate programs and propagates when those programs run. Modern threats are diverse, including ransomware and trojans that don’t behave like traditional viruses. Use layered defenses: keep software patched, run endpoint protection, avoid suspicious attachments, and back up data so you can recover from infection." },
            { "purpose", "My purpose is to increase people’s knowledge and understanding about cybersecurity, helping you stay safe online. I provide practical tips, explain common threats, and suggest steps you can take to reduce risk. If you have specific scenarios or concerns, ask and I’ll give actionable guidance tailored to your situation." },
            { "how are you", "I’m doing well — thank you for asking! I’m here to help you learn about cybersecurity and answer questions. Tell me a topic you’d like to explore and I’ll provide guidance, examples, and resources to help you stay safe online." },
            { "what can i ask", "You can ask me about cybersecurity topics such as passwords, phishing, malware, safe browsing, scams, identity theft, social media safety, two-factor authentication, software updates, public Wi-Fi, computer viruses, and also practical steps for securing personal devices and accounts. If you have a real-world scenario, describe it and I’ll help you analyze risks and protections." }
        };

        public string GetResponse(string input, string userName)
        {
            input = input.ToLower();

            var synonyms = new Dictionary<string, string>
            {
                // removed mapping to "scam" so fraud/con won't resolve to a removed topic
                { "hackers", "malware" }, { "hacker", "malware" },
                { "spyware", "malware" }, { "trojan", "malware" },
                { "identity", "identity theft" }, { "social", "social media" },
                { "2fa", "two factor" }, { "authentication", "two factor" },
                { "update", "updates" }, { "patch", "updates" },
                { "internet", "wifi" }, { "network", "wifi" }
            };

            foreach (var kvp in synonyms)
            {
                if (input.Contains(kvp.Key))
                    return Personalize(userName, topics[kvp.Value]);
            }

            foreach (var topic in topics.Keys)
            {
                int distance = LevenshteinDistance(input, topic);
                if (distance > 0 && distance <= 2)
                {
                    Console.WriteLine($"Did you mean '{topic}'? (yes/no)");
                    var confirmation = Console.ReadLine()?.Trim().ToLower();
                    if (confirmation == "yes") return Personalize(userName, topics[topic]);
                    else return Personalize(userName, "Alright, no problem. Could you rephrase your question?");
                }

                foreach (var word in input.Split(' '))
                {
                    int wordDistance = LevenshteinDistance(word, topic);
                    if (wordDistance > 0 && wordDistance <= 2)
                    {
                        Console.WriteLine($"Did you mean '{topic}'? (yes/no)");
                        var confirmation = Console.ReadLine()?.Trim().ToLower();
                        if (confirmation == "yes") return Personalize(userName, topics[topic]);
                        else return Personalize(userName, "Alright, no problem. Could you rephrase your question?");
                    }
                }
            }

            foreach (var kvp in topics)
            {
                if (input.Contains(kvp.Key))
                    return Personalize(userName, kvp.Value);
            }

            if (input.Contains("help"))
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("   📌 Topics you can ask me about:");
                Console.WriteLine("-----------------------------------------");
                int i = 1;
                foreach (var topic in topics.Keys)
                {
                    Console.WriteLine($"  {i++}. {topic}");
                }
                Console.WriteLine("=========================================");
                return Personalize(userName, "Type one of these topics to learn more!");
            }

            return Personalize(userName, "I didn’t quite understand that. Could you rephrase?");
        }

        private int LevenshteinDistance(string a, string b)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) dp[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) dp[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                                        dp[i - 1, j - 1] + cost);
                }
            }
            return dp[a.Length, b.Length];
        }

        private string Personalize(string userName, string content)
        {
            // Prefix every response with the chatbot persona and include the user's name
            if (string.IsNullOrWhiteSpace(userName))
                return $"Aphiwe here is what I found about this topic: {content}";
            return $"Aphiwe here is what I found about this topic, {userName}: {content}";
        }
    }
}
