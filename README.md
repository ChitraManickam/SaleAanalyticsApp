SaleAnalyticsApp
SaleAnalyticsApp is an ASP.NET Core Web API project for managing sales records with JWT authentication and Swagger documentation.

Prerequisites
Before running this project, ensure you have the following installed:

.NET 8 SDK
Visual Studio 2022 or Visual Studio Code
Getting Started
Clone Repository
Clone the repository to your local machine:

bash
Copy code
git clone https://github.com/ChitraManickam/SaleAanalyticsApp
cd SaleAnalyticsApp
Configuration
AppSettings

Navigate to appsettings.json file.
Modify the DefaultConnection string with your SQL Server connection string.
json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server;Database=your-database;Trusted_Connection=True;"
}
To create Table to maniplate the data. Execute below query.
CREATE TABLE SaleRecord (
    Id INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    SaleDate DATETIME NOT NULL,
    Region NVARCHAR(MAX) NOT NULL,
    Active BIT NOT NULL,
    CreatedOn DATETIME NOT NULL
);

JWT Secret Key

Open appsettings.json.
Set the JwtSettings:secretKey value to a secure key for JWT authentication.
json
Copy code
"JwtSettings": {
  "secretKey": "34jxuasf766weer397rzkzjbcjhvxcgha67qshdc"
}
Database Migration
Apply Entity Framework Core migrations to create/update the database: or directly used

bash
Copy code
dotnet ef database update
Project File (SaleAnalyticsApp.csproj)
xml
Copy code
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>false</InvariantGlobalization>
  </PropertyGroup>

  <ItemGroup>
    <!-- Packages -->
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.6" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.6">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.6" />
    <PackageReference Include="NLog" Version="5.3.2" />
    <PackageReference Include="NLog.Web.AspNetCore" Version="5.3.11" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>

</Project>
Running the Application
Visual Studio:
Open SaleAnalyticsApp.sln solution file.
Set the startup project to SaleAnalyticsApp.
Press F5 to build and run the project.
Command Line:
Navigate to your project directory and run:

bash
Copy code
dotnet run --project SaleAnalyticsApp/SaleAnalyticsApp.csproj or use shortkey Ctrl F5
API Documentation
Explore the API endpoints using Swagger:

Launch the application.
Navigate to https://localhost:7057/swagger/index.html in your web browser. This is my Local url.
Logging
Logging is configured using NLog with the following settings:

NLog Configuration: NLog.config
Log Files: Located in D:\DotNetAssessment\SaleAanalyticsApp-yyyy-MM-dd.txt
Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

License
This project is licensed under the MIT License.
