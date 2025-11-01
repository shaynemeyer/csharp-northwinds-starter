# C# Northwind Workshop - Complete Package

Welcome to the comprehensive C# and OOP workshop based on the classic Northwind database! This workshop is designed for developers using **VSCode** on **macOS, Linux, or Windows**.

## 📦 What's Included

This package contains everything you need to master C# and Object-Oriented Programming in web development:

### 📚 Documentation

1. **[docs/CSharp-Northwind-Workshop.md](docs/CSharp-Northwind-Workshop.md)** - Main workshop guide
   - Complete step-by-step tutorial
   - All code examples with explanations
   - OOP concepts explained in context
   - Exercises and advanced topics

2. **[docs/VSCode-QuickStart-Guide.md](docs/VSCode-QuickStart-Guide.md)** - VSCode setup guide
   - Installation instructions for all platforms
   - VSCode extensions to install
   - Keyboard shortcuts cheat sheet
   - Troubleshooting common issues

3. **[docs/Tailwind-CSS-Setup-Guide.md](docs/Tailwind-CSS-Setup-Guide.md)** - Tailwind CSS setup
   - Complete Tailwind installation guide
   - Configuration for ASP.NET Core
   - Utility classes reference
   - Common patterns and examples

4. **[docs/NextJS-vs-CSharp-Comparison.md](docs/NextJS-vs-CSharp-Comparison.md)** - Tech comparison
   - Side-by-side comparison with Next.js version
   - When to choose each technology
   - Code pattern mappings

### 🛠️ Setup Scripts

5. **[scripts/setup-northwind-workshop.sh](scripts/setup-northwind-workshop.sh)** - Bash script (macOS/Linux)
6. **[scripts/setup-northwind-workshop.bat](scripts/setup-northwind-workshop.bat)** - Batch script (Windows)

### ⚙️ Configuration Files

7. **[.vscode/](.vscode/)** - VSCode configuration
   - `launch.json` - Debug configurations
   - `tasks.json` - Build and migration tasks
   - `settings.json` - Recommended VSCode settings

## 🚀 Quick Start (5 Minutes)

### 1. Prerequisites

✅ Install these first:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/)
- VSCode C# Dev Kit extension

### 2. Create Your Project

**macOS/Linux:**
```bash
# Make the script executable
chmod +x scripts/setup-northwind-workshop.sh

# Run the setup
./scripts/setup-northwind-workshop.sh

# Open in VSCode
code NorthwindWorkshop
```

**Windows:**
```powershell
# Run the setup
.\scripts\setup-northwind-workshop.bat

# Open in VSCode
code NorthwindWorkshop
```

### 3. Start Learning

1. Open `docs/VSCode-QuickStart-Guide.md` - Get your environment ready
2. Open `docs/CSharp-Northwind-Workshop.md` - Start the workshop
3. Follow along step-by-step
4. Build your application!

## 📖 Workshop Structure

### Part 1: Setting Up the Solution
- Create multi-project solution
- Configure project references
- Install NuGet packages

### Part 2: Creating Domain Entities
- Learn OOP fundamentals
- Build entity classes
- Implement inheritance and encapsulation

### Part 3: Repository Pattern & Interfaces
- Master abstraction and polymorphism
- Create generic repositories
- Implement SOLID principles

### Part 4: Data Access with Entity Framework Core
- Configure DbContext
- Use LINQ for queries
- Implement eager loading

### Part 5: Database Seeding
- Populate initial data
- Learn data initialization patterns

### Part 6: Web Application Configuration
- Dependency injection
- Configure services
- Set up middleware

### Part 7: Creating ViewModels and Pages
- Build Razor Pages
- Implement MVVM pattern
- Create interactive UI

### Part 8: Layout and Navigation
- Master layouts and partials
- Build responsive navigation

### Part 9: Running the Application
- Create and apply migrations
- Debug in VSCode
- Use hot reload

### Part 10: Advanced Topics & Exercises
- CRUD operations
- Order management
- Search functionality
- Dashboard with analytics
- Unit of Work pattern

## 🎯 Learning Outcomes

By completing this workshop, you will:

### OOP Mastery
- ✅ **Encapsulation** - Properties, access modifiers, information hiding
- ✅ **Inheritance** - Base classes, derived classes, polymorphic behavior
- ✅ **Polymorphism** - Interfaces, virtual methods, method overriding
- ✅ **Abstraction** - Interfaces, abstract classes, design patterns

### SOLID Principles
- ✅ **S**ingle Responsibility Principle
- ✅ **O**pen/Closed Principle
- ✅ **L**iskov Substitution Principle
- ✅ **I**nterface Segregation Principle
- ✅ **D**ependency Inversion Principle

### Technical Skills
- ✅ ASP.NET Core web development
- ✅ Entity Framework Core ORM
- ✅ Repository pattern
- ✅ Dependency injection
- ✅ LINQ queries
- ✅ Async/await patterns
- ✅ Razor Pages
- ✅ Clean Architecture

## 🔧 Technology Stack

| Component | Technology |
|-----------|-----------|
| Framework | ASP.NET Core 9.0 |
| Language | C# 13 |
| Database | SQLite |
| ORM | Entity Framework Core |
| UI | Razor Pages + Tailwind CSS 4 |
| Node.js | 22+ (for Tailwind CSS) |
| IDE | Visual Studio Code |
| Platform | Cross-platform (macOS, Linux, Windows) |

## 📁 Project Architecture

```
NorthwindWorkshop/
├── NorthwindWorkshop.Core/          # Domain Layer
│   ├── Entities/                    # Domain entities
│   ├── Interfaces/                  # Repository interfaces
│   └── Services/                    # Business logic
│
├── NorthwindWorkshop.Infrastructure/  # Data Access Layer
│   ├── Data/                         # DbContext & seeding
│   ├── Repositories/                 # Repository implementations
│   └── Migrations/                   # EF Core migrations
│
└── NorthwindWorkshop.Web/            # Presentation Layer
    ├── Pages/                        # Razor Pages
    ├── ViewModels/                   # View models
    └── wwwroot/                      # Static files
```

## 💡 Using the VSCode Configuration

After creating your project:

1. Copy VSCode config files:
   ```bash
   cp -r .vscode NorthwindWorkshop/
   ```

2. Or let VSCode create them automatically when you first debug

3. Customize to your needs

## 🎓 Learning Path

### Beginner Path (Follow Workshop)
1. Complete Parts 1-9 in order
2. Understand each concept before moving on
3. Type all code yourself (don't copy-paste!)
4. Experiment with modifications

### Intermediate Path (With Exercises)
1. Complete Parts 1-9
2. Do all exercises in Part 10
3. Add your own features
4. Refactor for better design

### Advanced Path (Independent Learning)
1. Complete the workshop
2. Add authentication and authorization
3. Create Web API endpoints
4. Build a separate Blazor or React frontend
5. Deploy to cloud (Azure/AWS)

## 📚 Additional Resources

### Official Documentation
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [.NET CLI Reference](https://docs.microsoft.com/dotnet/core/tools/)

### VSCode Resources
- [VSCode C# Docs](https://code.visualstudio.com/docs/languages/csharp)
- [C# Dev Kit Guide](https://code.visualstudio.com/docs/csharp/get-started)
- [Debugging in VSCode](https://code.visualstudio.com/docs/editor/debugging)

### Learning Resources
- [Microsoft Learn - C#](https://docs.microsoft.com/learn/paths/csharp-first-steps/)
- [Microsoft Learn - ASP.NET Core](https://docs.microsoft.com/learn/paths/aspnet-core-web-app/)
- [SOLID Principles](https://www.digitalocean.com/community/conceptual_articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)

## ❓ FAQ

### Q: I'm new to C#. Can I do this workshop?
**A:** Yes! This workshop is designed for C# beginners who understand basic programming concepts. Start with the VSCode Quick Start Guide, then follow the main workshop step by step.

### Q: Do I need Visual Studio?
**A:** No! This workshop uses VSCode, which works on all platforms. Visual Studio is not required.

### Q: Can I use this on Linux?
**A:** Absolutely! Everything works on macOS, Linux, and Windows.

### Q: How long will this take?
**A:** Plan for 8-12 hours to complete the core workshop. Additional exercises can take another 4-8 hours.

### Q: What if I get stuck?
**A:** Check the troubleshooting sections in the VSCode Quick Start Guide. All common issues are covered.

### Q: Can I skip the OOP explanations?
**A:** We don't recommend it! Understanding OOP is crucial for writing maintainable C# code. The explanations are integrated with practical examples.

### Q: Is this workshop free?
**A:** Yes! All materials are included.

## 🤝 Tips for Success

1. **Type, Don't Copy** - Typing code helps you learn
2. **Understand Before Moving On** - Don't rush through concepts
3. **Experiment** - Try modifying the code to see what happens
4. **Use the Debugger** - Set breakpoints and step through code
5. **Read Error Messages** - They usually tell you exactly what's wrong
6. **Take Breaks** - Complex concepts need time to sink in
7. **Build Something Extra** - Apply what you learn to your own ideas

## 🐛 Troubleshooting

### Common Issues

**"dotnet: command not found"**
- Restart your terminal after installing .NET SDK
- Check PATH environment variable

**IntelliSense not working**
- Wait for OmniSharp to initialize (check status bar)
- Reload VSCode window: `Cmd+Shift+P` > "Developer: Reload Window"

**Build errors**
- Run `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

**Can't run migrations**
- Install EF Core tools: `dotnet tool install --global dotnet-ef`
- Make sure you're in the Web project directory

See the VSCode Quick Start Guide for more troubleshooting.

## 🎯 What's Next?

After completing this workshop, consider:

1. **Build Your Own Project** - Apply what you learned
2. **Learn Unit Testing** - xUnit, NUnit, or MSTest
3. **Explore Web APIs** - Build RESTful APIs with ASP.NET Core
4. **Try Blazor** - Build SPAs with C#
5. **Learn Advanced EF Core** - Performance, migrations, advanced queries
6. **Study Design Patterns** - Gang of Four patterns in C#
7. **Deploy to Cloud** - Azure, AWS, or Docker containers

## 📄 File Guide

Quick reference to what each file does:

| File | Purpose |
|------|---------|
| `docs/CSharp-Northwind-Workshop.md` | Main workshop - follow this! |
| `docs/VSCode-QuickStart-Guide.md` | Setup your development environment |
| `docs/NextJS-vs-CSharp-Comparison.md` | Understand the differences |
| `scripts/setup-northwind-workshop.sh` | Mac/Linux project setup |
| `scripts/setup-northwind-workshop.bat` | Windows project setup |
| `.vscode/` | VSCode configuration files |

## 🌟 Ready to Begin?

1. ✅ Make sure .NET SDK and VSCode are installed
2. ✅ Install VSCode C# Dev Kit extension
3. ✅ Run the setup script for your platform
4. ✅ Open `docs/VSCode-QuickStart-Guide.md`
5. ✅ Start the workshop!

---

## 📧 Support

If you have questions about C#, .NET, or this workshop:
- Check the troubleshooting sections
- Review Microsoft's official documentation
- The workshop includes detailed explanations for all concepts

## 📝 License

This workshop is designed for educational purposes. The Northwind database is a classic Microsoft sample database.

---

**Happy Learning! 🚀**

Master C# and Object-Oriented Programming by building a real application!
