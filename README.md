![Generic thumbnail](https://raw.githubusercontent.com/weto91/kindle-cover-fixer/main/Images/Cover.png)

<p align="center">
	<a>    
		<img alt="GitHub all releases" src="https://img.shields.io/github/downloads/weto91/kindle-cover-fixer/total">
	</a>
	<a>
		<img alt="GitHub Contributors" src="https://img.shields.io/github/contributors/weto91/kindle-cover-fixer" />
	</a>
    <a>
		<img alt="Issues" src="https://img.shields.io/github/issues/weto91/kindle-cover-fixer?color=0088ff" />
    </a>
    <a>
		<img alt="GitHub Repo stars" src="https://img.shields.io/github/stars/weto91/kindle-cover-fixer">    
    </a>    
    <a>
		<img alt="Github commits from last release" src="https://img.shields.io/github/commits-since/weto91/kindle-cover-fixer/latest/Develop">
    </a>
    <a>
		<img alt="GitHub last commit (by committer)" src="https://img.shields.io/github/last-commit/weto91/kindle-cover-fixer">
    </a>
    <a>
		<img alt="GitHub Release Date - Published_At" src="https://img.shields.io/github/release-date/weto91/kindle-cover-fixer">
    </a>
    <br />
    <br />
</p>

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
- Select the Calibre Library that you want to fix in your kindle.
- Click on "Find Books" button and :
- you will see a table with the complete list of books in your library. In this table you can see:
    - Book nº: A simple accountant.
    - Book Name: The name of the book
    - UUID: Universal Unique Identifier
    - Passed: If the book has UUID and COVER, it will be true and the Cover can be generated. Otherwise, nothing will be done with that book.
- Click on "Generate covers" button
- When the progress is complete, a new window will appear allowing you to transfer files directly to your Kindle device.
	- Please note that to transfer files to your device, it must be connected via USB to the PC. The application will detect it automatically.   
- Now, you can disconnect the device from your computer. The library on your Kindle have the covers fixed!

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
