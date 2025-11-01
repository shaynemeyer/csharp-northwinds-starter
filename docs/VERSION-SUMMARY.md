# Complete Version Summary

## Workshop Technology Versions

All workshop materials use the latest **stable, production-ready** versions of all technologies.

### Current Versions (All Stable)

| Technology | Version | Status | Released |
|------------|---------|--------|----------|
| **.NET SDK** | **9.0** | ✅ Stable | Nov 2024 |
| **C# Language** | **13** | ✅ Stable | Nov 2024 |
| **ASP.NET Core** | **9.0** | ✅ Stable | Nov 2024 |
| **Entity Framework Core** | **9.0** | ✅ Stable | Nov 2024 |
| **Tailwind CSS** | **4.0** | ✅ Stable | Dec 2024 |
| **Node.js** | **22+** | ✅ LTS | Oct 2024 |
| **Visual Studio Code** | Latest | ✅ Stable | Continuous |

## Installation Commands

### .NET 9.0 SDK

**Verify:**
```bash
dotnet --version
# Should show: 9.0.x
```

**Install:**
- **macOS**: `brew install --cask dotnet-sdk`
- **Linux**: `wget https://dot.net/v1/dotnet-install.sh && ./dotnet-install.sh --channel 9.0`
- **Windows**: Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

### Node.js 22+

**Verify:**
```bash
node --version
# Should show: v22.x.x or higher
```

**Install:**
- **macOS**: `brew install node` (will install latest LTS)
- **Linux**: `curl -fsSL https://deb.nodesource.com/setup_22.x | sudo -E bash - && sudo apt-get install -y nodejs`
- **Windows**: Download from [nodejs.org](https://nodejs.org) (choose LTS)

### Tailwind CSS 4.0

**Install:**
```bash
npm install tailwindcss @tailwindcss/cli
```

**Verify:**
```bash
npm list tailwindcss
# Should show: tailwindcss@4.0.x
```

## Why These Versions?

### .NET 9.0
- Latest stable version
- C# 13 language features
- Performance improvements
- Better tooling support
- Standard Term Support (STS) until May 2026

### Node.js 22
- Latest LTS (Long Term Support)
- Better performance
- Security updates
- Required for Tailwind CSS 4
- Support until April 2027

### Tailwind CSS 4.0
- Stable release (not beta)
- CSS-first configuration
- 10x faster builds (Rust engine)
- Production-ready
- No breaking changes from usage perspective

## Version History

### What Changed from Original

| Technology | Original | Updated To | Reason |
|------------|----------|------------|--------|
| .NET | 8.0 | **9.0** | Latest stable version |
| C# | 12 | **13** | Comes with .NET 9 |
| Tailwind CSS | 3.x | **4.0** | Modern CSS-first approach |
| Node.js | Not specified | **22+** | LTS, required for Tailwind 4 |

## Compatibility Notes

### All Versions are Production-Ready

✅ **.NET 9.0** - Stable release, fully supported  
✅ **C# 13** - Stable, no experimental features used  
✅ **Tailwind CSS 4.0** - Stable release (NOT beta)  
✅ **Node.js 22** - LTS (Long Term Support)  

### No Breaking Changes

All the code in this workshop:
- Works identically across all platforms (macOS, Linux, Windows)
- Uses only stable, documented APIs
- Follows best practices
- Is production-ready

### Support Timeline

| Version | Release | End of Support |
|---------|---------|----------------|
| .NET 9.0 (STS) | Nov 2024 | May 2026 |
| .NET 8.0 (LTS) | Nov 2023 | Nov 2026 |
| Node.js 22 (LTS) | Oct 2024 | Apr 2027 |
| Tailwind CSS 4 | Dec 2024 | Ongoing |

**Note:** .NET 9 is STS (Standard Term Support - 18 months). For longer support, .NET 8 LTS (3 years) is also compatible with this workshop.

## Verification Checklist

Use this checklist to verify your installation:

```bash
# Check .NET version
dotnet --version
# Expected: 9.0.x

# Check Node.js version
node --version
# Expected: v22.x.x or higher

# Check npm version
npm --version
# Expected: 10.x.x or higher

# Check VSCode is installed
code --version
# Expected: Should show version number

# Create a test project
dotnet new console -n VersionTest
cd VersionTest
dotnet run
# Expected: "Hello, World!"

# Test Tailwind installation (in any directory)
npm install tailwindcss @tailwindcss/cli
npx tailwindcss --help
# Expected: Should show Tailwind help
```

## Troubleshooting

### "dotnet: command not found"
- Restart your terminal after installing .NET SDK
- Check PATH environment variable
- Reinstall .NET SDK

### "node: command not found"
- Install Node.js from [nodejs.org](https://nodejs.org)
- Use version 22 or higher (LTS)
- Restart terminal after installation

### "Tailwind not building"
- Make sure Node.js 22+ is installed
- Run `npm install` in your web project
- Check `package.json` has correct versions

### Version Mismatch
If you see warnings about version mismatches:
1. Update to the specified versions
2. Run `dotnet restore` and `npm install`
3. Rebuild: `dotnet clean && dotnet build`

## Migration from Older Versions

### From .NET 8 to .NET 9
See [DOTNET-9-UPDATE.md](DOTNET-9-UPDATE.md) for complete migration guide.

### From Tailwind 3 to Tailwind 4
See [TAILWIND-4-UPDATE.md](TAILWIND-4-UPDATE.md) for complete migration guide.

### From Node.js 20 to Node.js 22
Node.js 22 is backwards compatible with Node.js 20. Simply:
1. Install Node.js 22 LTS
2. Run `npm install` in your project
3. Everything should work without changes

## Update Recommendations

### Keep Updated
- **Monthly**: Update NuGet packages (`dotnet list package --outdated`)
- **Monthly**: Update npm packages (`npm outdated`)
- **Quarterly**: Update VSCode and extensions
- **As needed**: Update .NET SDK (check for security updates)

### Breaking Changes
When updating in the future:
1. Check release notes first
2. Test in development before production
3. Update one major version at a time
4. Keep backups before major updates

## Resources

- [.NET 9 Release Notes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [C# 13 Features](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13)
- [Node.js 22 Release](https://nodejs.org/en/blog/release/v22.0.0)
- [Tailwind CSS 4 Documentation](https://tailwindcss.com/docs)

---

**All versions in this workshop are stable and production-ready!** ✅

You can confidently use this workshop to learn modern C# development with the latest tools.
