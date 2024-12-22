# Online Library

This is my pet-project. 

## Tech Stack

- C#
- ASP.NET Core
- Entity Framework
- MS SQL Server
- JS
- React

## Installation

1. **Clone the Repository:** Begin by cloning this repository to your local machine (open the terminal inside the folder you want to clone that repository in -> write):

     ```bash
   git clone https://github.com/ch4rniauski/LibraryWebApp.git
2. **Install Node.js:** To run that project you must have Node.js.  Download from the offical site `https://nodejs.org/en`
3. **Install dotnet tools:** To create migration of a database you must have a dotnet tools.  You can check the installation guide here `https://learn.microsoft.com/en-us/ef/core/cli/dotnet`
4. **Change the connection string:** Replace the connection string (open the project -> move to the appsettings.json -> find `LibraryDb`) with your database connction string.
5. **Update your database:** To update your database or create new table(if you didnt have it before) write that command inside the project folder using the Terminal:

     ```bash
   dotnet ef database update --startup-project LibraryWebApp --project Library.DataContext
6. **Run the server:** Open the project in Visual Studio or your preferred IDE. Build run the project and launch the server.
7. **Run the client:** Open the `frontend` folder via the terminal or VSCode. Then install all necessary files using that command:

     ```bash
   npm install
Then launch client using the that command:

     npm run dev
And you will see the adress you need to copy and paste in your web-browser
