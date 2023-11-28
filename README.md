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
		<img alt="Github commits from last release" src="https://img.shields.io/github/commits-since/weto91/kindle-cover-fixer/latest/main">
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
- Clean your Kindle of unused covers
- You won't have to do anything else to enjoy your covers again.

  THIS APPLICATION IS COMPATIBLE WITH ALL KINDLE MODELS.

## How to run the app
- You can download the last release from: https://github.com/weto91/kindle-cover-fixer/releases/latest
- There is a zip file that contains the application. Please review the project wiki for more information.

(This application is compatible from Windows 7 to the latest Windows version)

## How to use the application
- The first thing to do is transfer the books from  Calibre library to the Kindle device *(1)*
- You must disconnect the device and wait for it to load the complete library.
	- With the Kindle device connected to the internet, wait for the following image to appear as the cover image of our ebooks

  
![Generic thumbnail](https://raw.githubusercontent.com/weto91/kindle-cover-fixer/main/thumbnail_generic.jpg)

- Connect the device to the Windows PC *(2)*
- Open **Kindle Cover Fixer**
- Select the Calibre Library that you want to fix in your kindle.
- Click on "Find Books" button and :
- you will see a table with the complete list of books in your library. In this table you can see:
    - Book nº: A simple accountant.
    - Book Name: The name of the book
    - UUID: Universal Unique Identifier *(3)*
    - Is on Kindle: Yes if it is, no if not.
    - PROBLEMS: 3 errors may appear here:
    	- UUID: The book has not any UUID
		- COVER: The book has not any cover
		- FORMAT: The book has not the correct format (.azw3 or .mobi)
> You can learn how to fix each of these errors on the Wiki [(HERE)](https://github.com/weto91/kindle-cover-fixer/wiki "(HERE)")
- Click on "Generate covers" button *(4)*
- When the progress is complete, a new button will be enabled allowing you to transfer files directly to your Kindle device.
- Now, you can disconnect the device from your computer. The library on your Kindle have the covers fixed!

------------

>(1): The best way to make sure that the book is transferred correctly (to be able to generate the covers later and to have a better order in your library) is (For example, if we have our library in .mobi format):
> - Select all the books from your libraries in Caliber
> - Convert them to (e.g. .azw3)
> - We delete all .mobi
> - We convert all of them back to .mobi
> - We delete all .azw3
>(The .azw3 format is recommended, as it is more advanced, .mobi will disappear soon.)

>(2): It is necessary to connect and keep the Kindle connected while running the application.

>(3): If it appears blank, it will mean that the book does not have a UUID, in which case the cover will NOT be generated.

>(4): The application will only generate the covers of those books that:
> - Found on the Kindle
> - Have UUID
> - Have a cover
> - Have a correct format (.azw3 or .mobi)

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
## Modify the application yourself
You can modify the application as you want. This is the IDE that i use:
> Visual Studio 2022 Community.
- Open the Kindle Cover Fixer.sln file from Visual Studio 2022 Community. This way you can view the graphic editor of the forms.
- In case of forking the project. Modify the CheckGitHubNewerVersion function:
> var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");

TO

> var releases = await client.Repository.Release.GetLatest("your_GitHub_user", "kindle-cover-fixer");

This way, if you upload a new release, the versions will be checked from your own repository. You will also have to modify the MainScreen function. Exactly the variable versionLabel.Text. So that it works with your own version.

Keep in mind that the application is in a very early stage of development, so in addition to having possible bugs, the code is barely commented or organized. When we have a more stable version, I will dedicate myself 100% to solving these points.