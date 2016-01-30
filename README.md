This is a GUI version of youtube-dl found here:https://rg3.github.io/youtube-dl/

![Screenshot](https://cloud.githubusercontent.com/assets/12506856/9489445/bda99176-4bfe-11e5-830a-739db781d58a.PNG)

This software is the result of my realization that most people (including me) are just too lazy to use the command-line tool ['youtube-dl'](https://rg3.github.io/youtube-dl/). So I decided to build a GUI for it with only the most bare-bone features (youtube-dl has a ton of features which are not available in my version - maybe they will soon be available :p).

You can download videos from other video sharing sites as well. A huge set of sites are supported. To see the full list of supported sites [go here.](https://rg3.github.io/youtube-dl/supportedsites.html) The advanced options like quality etc. are only applicable to youtube at the moment; soon they will apply to other sites as well.

Here it is in action:
![screenshot](https://cloud.githubusercontent.com/assets/12506856/8923816/6b50f2ea-3513-11e5-9c7d-8e3f31a1f858.PNG)


###Building

I have used several packages, such as WindowsApiCodePack, Costura.Fordy etc, which NuGet should take care of automatically when building(See [here](https://docs.nuget.org/Consume/Package-Restore)). So clone the repo, and you're good to go.

**To download the executable**, go to **downloads** section under releases : https://github.com/rnand/Youtuble-downloader/releases

You need dot net framework 4 to run the application. Get it here: https://www.microsoft.com/en-in/download/details.aspx?id=17851

You also need ffmpeg for DASH videos. Get it here: https://www.ffmpeg.org/download.html

###USAGE

1. Copy youtube/vimeo/dailymotion/any-other-supported-site video url to clipboard

2. Open YouTube Downloader.exe

3. Select the necessary options

4. Click *Download*
