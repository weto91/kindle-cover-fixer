# Kindle Cover Fixer

## Features

- Fix your missing ebook covers in your Kindle library
- Send the fixed images to your Kindle Scribe device in one click.
- You won't have to do anything else to enjoy your covers again.

## How to run the app
- You can download the last release from: https://github.com/weto91/kindle-cover-fixer/releases/latest
- There is a setup file that can be used as normal application in your windows computer
(This application is compatible from Windows 7 to the last Windows version)

## How to use the application
- The first thing to do is transfer the books from our caliber library to the Kindle device
- Disconnect the device and wait for it to load the complete library
- With the Kindle device connected to the internet, wait for the following image to appear as the cover image of our ebooks

  
![Generic thumbnail](https://raw.githubusercontent.com/weto91/kindle-cover-fixer/main/thumbnail_generic.jpg)

- Connect the device to the Windows PC
- Open the application
- Click on "Select" button and select the Calibre library folder on your HDD (It is normally in the Documents/Calibre library folder)
- After a few seconds, the complete list of books in the library will appear in the blank square of the application
- Click on "Generate covers" button
- When the progress is complete, a new window will appear allowing you to open the output directory with the corrected covers.
    - If you click on OK. the EXPORTED directory will open. This directory contains all the covers with their respective special names.   
- A new button will now have appeared to the left of the "Generate covers" button. If you press it while the Kindle Scribe is connected to the PC, the application will automatically transfer all the covers to the correct directory.
- Now, you can disconnect the Kindle Scribe from your computer. The library on your Kindle have the covers fixed!

## Modify the application yourself
You can modify the application as you want. This is the IDE that i use:
> Visual Studio 2022 Community.
- Open the Kindle Cover Fixer.sln file from Visual Studio 2022 Community. This way you can view the graphic editor of the forms.
- In case of forking the project. Modify the CheckGitHubNewerVersion function:
> var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");

TO

> var releases = await client.Repository.Release.GetLatest("your_GitHub_user", "kindle-cover-fixer");

This way, if you upload a new release, the versions will be checked from your own repository. You will also have to modify the MainScreen function. Exactly the variable versionLabel.Text. So that it works with your own version.

## Compatible devices
At this time, this application is only tested on Kindle Scribe. In next updates, we will test it on:
- Kindle Paperwhite 7º Gen
- Kindle Paperwhite 11º Gen
- Kindle Oasis 10º Gen

> We do not have any more Kindle devices to test. If you want to participate in the development, contact me at: alruad@gmail.com

## Next steps
- Add compatibility to some another Kindle devices
- Add the functionality to automatically transfer fixed covers to the Kindle device. --> DONE
