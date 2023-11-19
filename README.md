# Kindle Cover Fixer

## Features

- Fix your missing ebook covers in your Kindle library
- Send the fixed images to your Kindle device in one click.
- You won't have to do anything else to enjoy your covers again.

  THIS APPLICATION IS COMPATIBLE WITH ALL KINDLE MODELS.

## How to run the app
- You can download the last release from: https://github.com/weto91/kindle-cover-fixer/releases/latest
- There is a setup file that can be used as normal application in your windows computer
(This application is compatible from Windows 7 to the last Windows version)

## How to use the application
- The first thing to do is transfer the books from our calibre library to the Kindle device
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
- A new button will now have appeared to the left of the "Generate covers" button.
- If you connected your Kindle to your PC before opening the app, you can press the "Transfer to Kindle" button and the app will transfer the newly generated covers to your device automatically.
- If you haven't connected your Kindle before opening the app, you can connect it and have it recognized by the app by pressing the "Connect Device" button.     
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
- Kindle Scribe
- Kindle Oasis (10. gen)
- Kindle Oasis (9. gen)
- Kindle Oasis (8. gen)
- Kindle Paperwhite (11. gen)
- Kindle Paperwhite (10. gen)
- Kindle Paperwhite (7. gen)
- Kindle Paperwhite (6. gen)
- Kindle Paperwhite (5. gen)
- Kindle (11. gen)
- Kindle (10. gen)
- Kindle (8. gen)
- Kindle (7. gen)
- Kindle (5. gen)
- Kindle (4. gen)
- Kindle (2. gen)
- Kindle (1. gen)
- Kindle Voyage (7. gen)
- Kindle Touch (4. gen)
  
(ALL MODELS)
