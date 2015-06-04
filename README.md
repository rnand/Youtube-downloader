This is a GUI version of youtube-dl found here:https://rg3.github.io/youtube-dl/


This software is the result of my realization that most people (including me) are too stupid or just lazy to use the command-line tool 'youtube-dl'. So I decided to build a GUI for it with only the most bare-bone features (youtube-dl has a ton of features which are not available in my version).

But the problem is youtube-dl is written in Python and I suck at it. Especially when it comes to building GUIs in Python. So I decided to use C# and WinForms. But I don't know how to execute Python scripts inside C# (I'm in the process of learning IronPython, which I hope will help me acheive this.*If anyone have a better idea, please share it with me*). So what I ended up doing was, compile the Python scripts to a binary executable, embedd that inside my C# program, and call it with arguments that the user enters through the GUI. This is an extreamely shady and sloppy and crappy way to do it, but it works!


**To download the executable**, go to **downloads** section under releases : https://github.com/rnand/Youtuble-downloader/releases

You need .net framework 4 to run the application.

You probably also need ffmpeg for DASH videos. Get it here: https://www.ffmpeg.org/download.html

**USAGE**

1. Copy youtube video url to clipboard

2. Open YouTube Downloader.exe

3. Select the necessary options

4. Click ***Download***