#!/bin/bash

# Northwind Workshop Setup Script
# This script creates the complete C# Northwind Workshop project structure

echo "🚀 Creating C# Northwind Workshop..."
echo ""

# Check if .NET SDK is installed
if ! command -v dotnet &> /dev/null
then
    echo "❌ .NET SDK not found!"
    echo ""
    echo "Please install .NET 9.0 SDK first:"
    echo "  macOS:   brew install --cask dotnet-sdk"
    echo "  Linux:   https://dotnet.microsoft.com/download"
    echo "  Windows: https://dotnet.microsoft.com/download"
    echo ""
    exit 1
fi

echo "✓ .NET SDK version: $(dotnet --version)"
echo ""

# Create solution
echo "📁 Creating solution..."
dotnet new sln -n NorthwindWorkshop

# Create projects
echo "📦 Creating projects..."
dotnet new classlib -n NorthwindWorkshop.Core
dotnet new classlib -n NorthwindWorkshop.Infrastructure
dotnet new webapp -n NorthwindWorkshop.Web

# Add projects to solution
echo "🔗 Adding projects to solution..."
dotnet sln add NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet sln add NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj
dotnet sln add NorthwindWorkshop.Web/NorthwindWorkshop.Web.csproj

# Set up project references
echo "📚 Setting up project references..."
cd NorthwindWorkshop.Infrastructure
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj

cd ../NorthwindWorkshop.Web
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet add reference ../NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj

# Install NuGet packages
echo "📥 Installing NuGet packages..."
cd ../NorthwindWorkshop.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

cd ../NorthwindWorkshop.Web
dotnet add package Microsoft.EntityFrameworkCore.Design

# Create folder structure for Core project
echo "📂 Creating Core project structure..."
cd ../NorthwindWorkshop.Core
mkdir -p Entities
mkdir -p Interfaces
mkdir -p Services

# Create folder structure for Infrastructure project
echo "📂 Creating Infrastructure project structure..."
cd ../NorthwindWorkshop.Infrastructure
mkdir -p Data
mkdir -p Repositories
mkdir -p Migrations

# Create folder structure for Web project
echo "📂 Creating Web project structure..."
cd ../NorthwindWorkshop.Web
mkdir -p Pages/Customers
mkdir -p Pages/Products
mkdir -p Pages/Employees
mkdir -p Pages/Orders
mkdir -p Pages/Categories
mkdir -p Pages/Suppliers
mkdir -p ViewModels
mkdir -p wwwroot/css
mkdir -p wwwroot/js

cd ..

# Check if EF Core tools are installed
echo ""
echo "🔧 Checking for EF Core tools..."
if ! dotnet tool list -g | grep -q "dotnet-ef"; then
    echo "Installing EF Core tools globally..."
    dotnet tool install --global dotnet-ef
else
    echo "✓ EF Core tools already installed"
fi

echo ""
echo "✅ Project structure created successfully!"
echo ""
echo "📝 Next steps:"
echo "   1. Open in VSCode: code ."
echo "   2. Install Tailwind CSS (see Tailwind-CSS-Setup-Guide.md)"
echo "   3. Follow the workshop guide (CSharp-Northwind-Workshop.md)"
echo "   4. Start with Part 2: Creating Domain Entities"
echo ""
echo "🎓 Happy learning!"
