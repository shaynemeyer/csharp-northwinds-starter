@echo off
REM Northwind Workshop Setup Script for Windows
REM This script creates the complete C# Northwind Workshop project structure

echo.
echo Creating C# Northwind Workshop...
echo.

REM Create solution
echo Creating solution...
dotnet new sln -n NorthwindWorkshop

REM Create projects
echo Creating projects...
dotnet new classlib -n NorthwindWorkshop.Core
dotnet new classlib -n NorthwindWorkshop.Infrastructure
dotnet new webapp -n NorthwindWorkshop.Web

REM Add projects to solution
echo Adding projects to solution...
dotnet sln add NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet sln add NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj
dotnet sln add NorthwindWorkshop.Web/NorthwindWorkshop.Web.csproj

REM Set up project references
echo Setting up project references...
cd NorthwindWorkshop.Infrastructure
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj

cd ../NorthwindWorkshop.Web
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet add reference ../NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj

REM Install NuGet packages
echo Installing NuGet packages...
cd ../NorthwindWorkshop.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

cd ../NorthwindWorkshop.Web
dotnet add package Microsoft.EntityFrameworkCore.Design

REM Create folder structure for Core project
echo Creating Core project structure...
cd ../NorthwindWorkshop.Core
mkdir Entities
mkdir Interfaces
mkdir Services

REM Create folder structure for Infrastructure project
echo Creating Infrastructure project structure...
cd ../NorthwindWorkshop.Infrastructure
mkdir Data
mkdir Repositories
mkdir Migrations

REM Create folder structure for Web project
echo Creating Web project structure...
cd ../NorthwindWorkshop.Web
mkdir Pages\Customers
mkdir Pages\Products
mkdir Pages\Employees
mkdir Pages\Orders
mkdir Pages\Categories
mkdir Pages\Suppliers
mkdir ViewModels
mkdir wwwroot\css
mkdir wwwroot\js

cd ..

echo.
echo Project structure created successfully!
echo.
echo Next steps:
echo    1. Open in VSCode: code .
echo    2. Install Tailwind CSS (see Tailwind-CSS-Setup-Guide.md)
echo    3. Follow the workshop guide (CSharp-Northwind-Workshop.md)
echo    4. Start with Part 2: Creating Domain Entities
echo.
echo Happy learning!
echo.
pause
