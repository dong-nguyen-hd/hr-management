# Web API Project:<p/>Human Resource Management for IT Company

This project building on **.NET 10**, using for Human Resource Management for IT Company.

## Technologies

* [ASP.NET Core API 10](https://learn.microsoft.com/en-us/aspnet/core/)
* [Entity Framework Core 10](https://docs.microsoft.com/en-us/ef/core/)
* [Cronos](https://github.com/HangfireIO/Cronos)
* [Serilog](https://serilog.net/)
* [EPPlus](https://github.com/EPPlusSoftware/EPPlus)
* [xUnit Test](https://xunit.net/)
* [Quasar](https://quasar.dev/)
* [SixLabors.ImageSharp](https://docs.sixlabors.com/index.html)
* [AutoMapper](https://automapper.org/)
* [Swagger UI - Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Features
* Human resources management with following information: personal, work history, project, certificate, education, skill, timesheet, salary,...
* Support: authentication-authorization with token-base (JWT).
* Archiving accounts into database, hashing password-salt with PBKDF2 algorithm.
* Storing and resizing image.
* Import excel file.
* Getting data with pagination.
* Logging information to console and file.
* Cron-job supports cleaning refresh-token.

## Diagram

![](/Docs/diagram.png)

## Design Patterns

* Unit of Work
* Generic Repository
* Request-Reply
* Inversion of Control / Dependency injection

## Getting Started

* Following CLI:

**Note:** you must directive to root project before run CLI.
1. Install [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download)
2. Install [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. Editing connection string in **appsettings.json**
4. Run `cd ./Infrastructure`.
5. Run `dotnet ef --startup-project ../API/ migrations add Initial` to add migration.
6. Run `dotnet ef --startup-project ../API/ database update` to update database.
7. Run `cd ../API`.
8. Run `dotnet run` to runs source code without any explicit compile or launch commands.

* Seeding data: 

1. First login: [http://yourhost/api/v1/token/login]()
 + Username: **admin**
 + Password: **c93ccd78b2076528346216b3b2f701e6** (plain-text: admin1234) (hash function: MD5)
2. Get your access token

## License

This project is licensed with the [MIT license](LICENSE).