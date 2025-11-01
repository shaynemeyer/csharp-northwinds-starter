# .NET 9.0 Update Summary

## What Changed

All workshop materials have been updated from **.NET 8.0** to **.NET 9.0**!

## Updated Versions

| Component | Previous | Updated |
|-----------|----------|---------|
| .NET SDK | 8.0 | **9.0** |
| ASP.NET Core | 8.0 | **9.0** |
| C# Language | 12 | **13** |
| Target Framework | net8.0 | **net9.0** |

## Files Updated

All documentation and configuration files have been updated:

### Documentation Files
- ✅ CSharp-Northwind-Workshop.md
- ✅ README.md
- ✅ START-HERE.txt
- ✅ VSCode-QuickStart-Guide.md
- ✅ NextJS-vs-CSharp-Comparison.md
- ✅ Workshop-Checklist.md
- ✅ Tailwind-CSS-Setup-Guide.md
- ✅ File-Navigation-Guide.md

### Setup Scripts
- ✅ setup-northwind-workshop.sh
- ✅ setup-northwind-workshop.bat

### Configuration Files
- ✅ sample-vscode-config/launch.json

## What You Need to Do

### 1. Install .NET 9.0 SDK

**macOS:**
```bash
brew install --cask dotnet-sdk
```

**Linux (Ubuntu/Debian):**
```bash
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 9.0
```

**Windows:**
Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

**Verify installation:**
```bash
dotnet --version
# Should output: 9.0.x or higher
```

### 2. Update Existing Projects (If You Already Started)

If you already created a project with .NET 8, you can update it:

**Update project files:**

Edit all `.csproj` files and change:
```xml
<!-- Old -->
<TargetFramework>net8.0</TargetFramework>

<!-- New -->
<TargetFramework>net9.0</TargetFramework>
```

**Files to update:**
- `NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj`
- `NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj`
- `NorthwindWorkshop.Web/NorthwindWorkshop.Web.csproj`

**Then restore and rebuild:**
```bash
dotnet restore
dotnet build
```

### 3. Update VSCode Configuration

If you have a `.vscode/launch.json` file, update the program path:

```json
{
  "program": "${workspaceFolder}/NorthwindWorkshop.Web/bin/Debug/net9.0/NorthwindWorkshop.Web.dll"
}
```

## What's New in .NET 9.0

### Performance Improvements
- Faster startup times
- Reduced memory usage
- Improved JIT compilation

### C# 13 Features
- **params collections** - More flexible params parameters
- **Collection expressions** - Enhanced collection initialization
- **Ref struct improvements** - Better ref struct support
- **Primary constructors** - For all struct types
- **Extension types** - New extension member syntax

### ASP.NET Core 9.0 Features
- **Built-in OpenAPI support** - Native Swagger/OpenAPI generation
- **Enhanced minimal APIs** - Better routing and validation
- **Improved performance** - Faster request processing
- **Better diagnostics** - Enhanced debugging tools
- **Native AOT support** - Improved ahead-of-time compilation

### Entity Framework Core 9.0
- **Better LINQ translation** - More queries run on the database
- **Improved performance** - Faster query execution
- **Complex type support** - Better value object handling
- **JSON improvements** - Enhanced JSON column support

## Compatibility Notes

### Breaking Changes
.NET 9.0 is generally backwards compatible with .NET 8.0, but there are a few minor breaking changes:

1. **Some obsolete APIs removed** - Update your code if using deprecated features
2. **Default behavior changes** - Check ASP.NET Core middleware ordering
3. **NuGet package updates** - Some packages may need version updates

### Recommended Actions
1. Update all NuGet packages to their latest versions
2. Test your application thoroughly after upgrading
3. Review the [.NET 9 breaking changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0) documentation

## Workshop Impact

### No Impact on Learning Objectives
All OOP concepts, patterns, and architecture principles remain the same. The upgrade to .NET 9.0 provides:

✅ **Better performance** - Faster development and runtime  
✅ **Latest features** - Access to C# 13 and .NET 9 improvements  
✅ **Long-term support** - .NET 9 is a Standard Term Support (STS) release  
✅ **Modern practices** - Learn with the latest version  

### Code Remains Compatible
All code examples in the workshop work identically in .NET 9.0. You'll notice:
- Faster build times
- Better IntelliSense
- Improved error messages
- Enhanced tooling support

## Support Timeline

| Version | Release | End of Support |
|---------|---------|----------------|
| .NET 8.0 (LTS) | Nov 2023 | Nov 2026 |
| **.NET 9.0 (STS)** | **Nov 2024** | **May 2026** |
| .NET 10.0 (LTS) | Nov 2025 | Nov 2028 |

**Note:** .NET 9.0 is a Standard Term Support release (18 months). .NET 8.0 has Long Term Support (3 years).

## Migration Guide

### For New Users
Simply follow the workshop as written. All instructions now reference .NET 9.0.

### For Existing Users
1. **Backup your project**
2. **Install .NET 9.0 SDK**
3. **Update .csproj files** (change net8.0 to net9.0)
4. **Update NuGet packages**:
   ```bash
   dotnet list package --outdated
   dotnet add package [PackageName]
   ```
5. **Clean and rebuild**:
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   ```
6. **Test your application**

## Resources

- [.NET 9.0 Announcement](https://devblogs.microsoft.com/dotnet/announcing-dotnet-9/)
- [What's New in .NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [C# 13 Features](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13)
- [ASP.NET Core 9.0](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0)
- [EF Core 9.0](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)

## Verification

Check that everything is updated correctly:

```bash
# Check .NET version
dotnet --version

# Check project target framework
dotnet list package --framework

# Build the solution
dotnet build

# Run the application
dotnet run --project NorthwindWorkshop.Web
```

Expected output:
```
.NET Version: 9.0.x
Build succeeded.
Now listening on: https://localhost:5001
```

---

**Your workshop is now running on .NET 9.0! 🚀**

All learning objectives remain the same, but you're now using the latest and greatest version of .NET!
