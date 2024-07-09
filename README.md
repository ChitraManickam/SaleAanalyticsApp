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

____________________________________________________________________________________________________________________________________________________

This is **Postman Collection .json** format file for test the endpoints.

_________________________________________________________Start Postman Collection___________________________________________________-
{
	"info": {
		"_postman_id": "623aa7cb-c0cb-4801-9589-72c17ddb2c8e",
		"name": "SaleAnalyticsApp-API",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "26258944",
		"_collection_link": "https://dark-crater-137684.postman.co/workspace/Team-Workspace~16526ee4-5905-4d38-8ebc-6ac4b860821f/collection/26258944-623aa7cb-c0cb-4801-9589-72c17ddb2c8e?action=share&source=collection_link&creator=26258944"
	},
	"item": [
		{
			"name": "Token",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTE0MjMsImV4cCI6MTcyMDUxNTAyMywiaWF0IjoxNzIwNTExNDIzfQ.0zzKew9DBJ3KJ73lC6LLdbzSbd-Rmq1rFOIvAels1hE",
							"type": "string"
						}
					]
				},
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"secretKey\": \"34jxuasf766weer397rzkzjbcjhvxcgha67qshdc\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7057/api/Token",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"Token"
					]
				}
			},
			"response": []
		},
		{
			"name": "Create SaleRecord",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MDgzMTUsImV4cCI6MTcyMDUxMTkxNSwiaWF0IjoxNzIwNTA4MzE1fQ.Xoykyb6c_2VAj4LO8Uz9gEJs2xgOOvcBigELgFGPJLs",
							"type": "string"
						}
					]
				},
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"id\": 0,\r\n  \"productName\": \"Monitor\",\r\n  \"price\": 200000,\r\n  \"saleDate\": \"2024-07-02T18:29:31.484Z\",\r\n  \"region\": \"Kovai\",\r\n  \"active\": true,\r\n  \"createdOn\": \"2024-07-02T18:29:31.484Z\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7057/api/SaleRecords/CreateSaleRecord",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"SaleRecords",
						"CreateSaleRecord"
					]
				}
			},
			"response": []
		},
		{
			"name": "Update SaleRecord By Id",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTA1MDUsImV4cCI6MTcyMDUxNDEwNSwiaWF0IjoxNzIwNTEwNTA1fQ.17KSV_2g1VQgEPVtJbdwoW3Evjgo_HuNiMqTQwfoZv8",
							"type": "string"
						}
					]
				},
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"id\": 4,\r\n  \"productName\": \"iPhone\",\r\n  \"price\": 55530,\r\n  \"saleDate\": \"2024-07-09T07:32:52.754Z\",\r\n  \"region\": \"Chennai\",\r\n  \"active\": true,\r\n  \"createdOn\": \"2024-07-09T07:32:52.754Z\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7057/api/SaleRecords/UpdateSaleRecordById?id=4\n",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"SaleRecords",
						"UpdateSaleRecordById"
					],
					"query": [
						{
							"key": "id",
							"value": "4\n"
						}
					]
				}
			},
			"response": []
		},
		{
			"name": "Delete SaleRecord By Id",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTA1MDUsImV4cCI6MTcyMDUxNDEwNSwiaWF0IjoxNzIwNTEwNTA1fQ.17KSV_2g1VQgEPVtJbdwoW3Evjgo_HuNiMqTQwfoZv8",
							"type": "string"
						}
					]
				},
				"method": "DELETE",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "1",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7057/api/SaleRecords/DeleteSaleRecordId",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"SaleRecords",
						"DeleteSaleRecordId"
					]
				}
			},
			"response": []
		},
		{
			"name": "Get SaleRecord By Id",
			"protocolProfileBehavior": {
				"disableBodyPruning": true
			},
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTE0MjMsImV4cCI6MTcyMDUxNTAyMywiaWF0IjoxNzIwNTExNDIzfQ.0zzKew9DBJ3KJ73lC6LLdbzSbd-Rmq1rFOIvAels1hE",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"body": {
					"mode": "urlencoded",
					"urlencoded": []
				},
				"url": {
					"raw": "https://localhost:7057/api/SaleRecords/GetId?id=8",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"SaleRecords",
						"GetId"
					],
					"query": [
						{
							"key": "id",
							"value": "8"
						}
					]
				}
			},
			"response": []
		},
		{
			"name": "Get All SaleRecords",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTE1OTAsImV4cCI6MTcyMDUxNTE5MCwiaWF0IjoxNzIwNTExNTkwfQ.KWajjfB0sqoqzkCaf7WdsuPNKDxy-tOMqb6gJ3aBfHA",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "https://localhost:7057/api/SaleRecords/GetAllSaleRecords",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"SaleRecords",
						"GetAllSaleRecords"
					]
				}
			},
			"response": []
		},
		{
			"name": "Total Sales",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTIxNzcsImV4cCI6MTcyMDUxNTc3NywiaWF0IjoxNzIwNTEyMTc3fQ.9iXMVsNhzbWKiUWaU3ddgWzeoiP4bDVGled6B8BKNC4",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "https://localhost:7057/api/Analytics/Total-Sales?startDate=2024-07-02 07:32:52.753&endDate=2024-07-09 07:32:52.753",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"Analytics",
						"Total-Sales"
					],
					"query": [
						{
							"key": "startDate",
							"value": "2024-07-02 07:32:52.753"
						},
						{
							"key": "endDate",
							"value": "2024-07-09 07:32:52.753"
						}
					]
				}
			},
			"response": []
		},
		{
			"name": "Sales Trends",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTIxNzcsImV4cCI6MTcyMDUxNTc3NywiaWF0IjoxNzIwNTEyMTc3fQ.9iXMVsNhzbWKiUWaU3ddgWzeoiP4bDVGled6B8BKNC4",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "https://localhost:7057/api/Analytics/Sales-Trends",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"Analytics",
						"Sales-Trends"
					]
				}
			},
			"response": []
		},
		{
			"name": "Top Products",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTI0MDQsImV4cCI6MTcyMDUxNjAwNCwiaWF0IjoxNzIwNTEyNDA0fQ.J3SELwkrD5uPkxFrjTw7__GEw_yDMcmFRKwGwOBf934",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "https://localhost:7057/api/Analytics/Top-Products?startDate=2024-07-02 07:32:52.753&endDate=2024-07-09 07:32:52.753",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"Analytics",
						"Top-Products"
					],
					"query": [
						{
							"key": "startDate",
							"value": "2024-07-02 07:32:52.753"
						},
						{
							"key": "endDate",
							"value": "2024-07-09 07:32:52.753"
						}
					]
				}
			},
			"response": []
		},
		{
			"name": "Sales By Region",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MjA1MTI0MDQsImV4cCI6MTcyMDUxNjAwNCwiaWF0IjoxNzIwNTEyNDA0fQ.J3SELwkrD5uPkxFrjTw7__GEw_yDMcmFRKwGwOBf934",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "https://localhost:7057/api/Analytics/Sales-By-Region",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7057",
					"path": [
						"api",
						"Analytics",
						"Sales-By-Region"
					]
				}
			},
			"response": []
		}
	]
}
_________________________________________________________END Postman Collection___________________________________________________-
