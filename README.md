# RoverRewards

RoverRewards is a rewards program for students. By attending school events, such as sports matches or band concerts, students can earn Treats, which are rewards points that can be used to purchase rewards. Administrators can track these stats, including the ability to add/remove these students, create events, and create rewards.


**Project Features**: 
- Admin Navigation Bar
- CRUD Users
- CRUD Events
- CRUD Roles
- CRUD Rewards
- Light/Dark Modes
- Add Attendees


## Prerequisites
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with ASP.NET and web development workload
- [Latest .NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
-   Install the latest [.NET & EF CLI Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) by using this command :

    ```.NET Core CLI
    dotnet tool install --global dotnet-ef
    ```
- Git Client 
  - [GitHub Desktop](https://desktop.github.com/)
  - [Axosoft's GitKraken](https://www.gitkraken.com/)


## Setup

To create a new project create a directory and open a command line console (cmd).  Change the directory to your working folder with the following command (assuming your working folder is c:\users\\**_username_**\\documents\\)

- Download ZIP from GitHub // OR // Open in Visual Studio
- Open the .sln file or save the file to your computer from VS


## Try it out!

Here's what you need to get your project running:
- Open the Solution in Visual Studio 2022
- Initialize the SQL Server Express LocalDB
  - In Visual Studio, go to `View > Other Windows > Package Manager Console`
  - In the console that appears at the bottom, type the command `Update-Database` and wait for the migration to finish.
- Run the project
  - Press `Control + F5` to run the project without the debugger, or `F5` to run the project with the debugger attached.
- Seed Data
  - When running the project for the first time, the database will be seeded with an admin user.
  - You can log in to this account with the username `admin` and the password `Password123!`. It is _highly_ recommended that you change this password after logging in for the first time.
