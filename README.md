## PROG6221-PART-1---CHATBOT
## Overview:
This project is a C# Console Chatbot designed to:

Greet the user with a voice message 

Display ASCII art for a cybersecurity-themed logo

Interact with the user through personalized input

Respond to basic questions (general + cybersecurity-related)

Provide a visually enhanced console UI with colors, borders, and typing effects

Be structured into multiple classes for clarity and maintainability

Be uploaded to GitHub with CI/CD integration

## Step-by-Step Breakdown:
1. Voice Greeting (Very Important)
Record or generate a voice greeting:

## Example message:
“Hello! Welcome to the Cybersecurity Awareness Bot. I’m here to help you stay safe online.”

Save as greeting.wav in /bin/Debug/

Code plays the audio automatically when the program starts.

## Tools:

Windows Voice Recorder (preferred)

Text-to-Speech sites (ttsmp3.com, fakeyou.com, naturalreaders.com)

Convert MP3 → WAV using cloudconvert.com

2. ASCII Art (Very Important)
Generate ASCII art using:

TAAG

TextKool

3. User Interaction
Ask for user’s name and personalize responses.

4. Basic Response System
Respond to:

“How are you?”

“What is your purpose?”

Cybersecurity questions (e.g., phishing, strong passwords, malware).

6. Enhanced Console UI
Use:

Colors (Console.ForegroundColor)

Borders (ASCII box styles)

Typing effect (loop with Thread.Sleep)

7. Code Structure (Very Important)
Correct structure:

Program.cs → Entry point

Chatbot.cs → Core chatbot logic

User.cs → User data handling

AudioPlayer.cs → Voice greeting playback

5. Input Validation
Handle:

Empty input

Unknown input

CyberBot/
│── Program.cs
│── Chatbot.cs
│── User.cs
│── AudioPlayer.cs
│── greeting.wav
│── README.md
│── .github/workflows/dotnet.yml

## Future Improvements:
Add more cybersecurity topics (phishing, ransomware, social engineering)

Expand chatbot responses with AI integration

Improve UI with animations

##
