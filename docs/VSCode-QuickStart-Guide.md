# VSCode Quick Start Guide - Northwind Workshop

This guide will get you up and running with the Northwind C# Workshop using VSCode on macOS, Linux, or Windows.

## Prerequisites Checklist

- [ ] .NET 9.0 SDK installed
- [ ] Visual Studio Code installed
- [ ] VSCode C# Dev Kit extension installed
- [ ] Terminal/Command Line access

## Step-by-Step Setup (10 minutes)

### 1. Install .NET SDK

**macOS:**
```bash
# Using Homebrew (recommended)
brew install --cask dotnet-sdk

# Verify installation
dotnet --version
```

**Linux (Ubuntu/Debian):**
```bash
# Download installer
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 9.0

# Add to PATH (add to ~/.bashrc or ~/.zshrc)
export PATH="$HOME/.dotnet:$PATH"
export DOTNET_ROOT="$HOME/.dotnet"

# Verify installation
dotnet --version
```

**Windows:**
1. Download installer from: https://dotnet.microsoft.com/download
2. Run the installer
3. Open new terminal and verify: `dotnet --version`

### 2. Install VSCode Extensions

Open VSCode and press:
- **macOS**: `Cmd+Shift+X`
- **Windows/Linux**: `Ctrl+Shift+X`

Install these extensions:

1. **C# Dev Kit** (ms-dotnettools.csdevkit)
   - Official C# support from Microsoft
   - Provides IntelliSense, debugging, project templates
   
2. **C#** (ms-dotnettools.csharp)
   - Core C# language support
   - Automatically installed with C# Dev Kit

3. **NuGet Package Manager** (jmrog.vscode-nuget-package-manager)
   - GUI for managing NuGet packages
   
4. **SQLite Viewer** (alexcvzz.vscode-sqlite) - Optional
   - View and query SQLite databases in VSCode

### 3. Create Your First Project

Open a terminal in VSCode (`Ctrl+`` or `Terminal > New Terminal`):

```bash
# Create a directory for your workshop
mkdir ~/NorthwindWorkshop
cd ~/NorthwindWorkshop

# Run the setup script (downloaded from the workshop)
chmod +x scripts/setup-northwind-workshop.sh
./scripts/setup-northwind-workshop.sh

# Or create manually:
dotnet new sln -n NorthwindWorkshop
dotnet new classlib -n NorthwindWorkshop.Core
dotnet new classlib -n NorthwindWorkshop.Infrastructure
dotnet new webapp -n NorthwindWorkshop.Web

# Add to solution
dotnet sln add **/*.csproj

# Set up references
cd NorthwindWorkshop.Infrastructure
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj

cd ../NorthwindWorkshop.Web
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet add reference ../NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj

cd ..
```

### 4. Open in VSCode

```bash
# From your solution directory
code .
```

**What happens next:**
- VSCode detects your .NET solution
- C# Dev Kit initializes (may take a moment first time)
- You'll see `.vscode/` folder created with debug configurations
- Solution Explorer appears in the sidebar

### 5. Install NuGet Packages

In VSCode terminal:

```bash
cd NorthwindWorkshop.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

cd ../NorthwindWorkshop.Web
dotnet add package Microsoft.EntityFrameworkCore.Design
```

**Or use the NuGet Package Manager extension:**
1. Open Command Palette: `Cmd+Shift+P` (Mac) or `Ctrl+Shift+P` (Win/Linux)
2. Type "NuGet: Manage NuGet Packages"
3. Select your project
4. Search and install packages

### 6. Install EF Core Tools

```bash
# Install globally (only needed once)
dotnet tool install --global dotnet-ef

# Verify installation
dotnet ef --version
```

### 7. Build Your First Entity

Create `NorthwindWorkshop.Core/Entities/BaseEntity.cs`:

```csharp
namespace NorthwindWorkshop.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
}
```

**VSCode Tips:**
- Use `Cmd+.` (Mac) or `Ctrl+.` (Win/Linux) for Quick Fixes
- Type `prop` and press `Tab` for property snippet
- `Ctrl+Space` for IntelliSense suggestions

### 8. Test Your Setup

Build the solution:

```bash
# From solution root
dotnet build
```

You should see:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

## VSCode Keyboard Shortcuts Cheat Sheet

### Navigation
| Action | macOS | Windows/Linux |
|--------|-------|---------------|
| Command Palette | `Cmd+Shift+P` | `Ctrl+Shift+P` |
| Quick Open File | `Cmd+P` | `Ctrl+P` |
| Go to Definition | `F12` | `F12` |
| Go Back | `Ctrl+-` | `Alt+Left` |
| Find References | `Shift+F12` | `Shift+F12` |
| Peek Definition | `Option+F12` | `Alt+F12` |

### Editing
| Action | macOS | Windows/Linux |
|--------|-------|---------------|
| Quick Fix | `Cmd+.` | `Ctrl+.` |
| Format Document | `Shift+Option+F` | `Shift+Alt+F` |
| Rename Symbol | `F2` | `F2` |
| Multi-cursor | `Option+Click` | `Alt+Click` |
| Comment Line | `Cmd+/` | `Ctrl+/` |

### Debugging
| Action | macOS | Windows/Linux |
|--------|-------|---------------|
| Start Debugging | `F5` | `F5` |
| Run Without Debug | `Ctrl+F5` | `Ctrl+F5` |
| Toggle Breakpoint | `F9` | `F9` |
| Step Over | `F10` | `F10` |
| Step Into | `F11` | `F11` |
| Stop Debugging | `Shift+F5` | `Shift+F5` |

### Terminal
| Action | macOS | Windows/Linux |
|--------|-------|---------------|
| New Terminal | `Ctrl+Shift+`` | `Ctrl+Shift+`` |
| Toggle Terminal | `Ctrl+`` | `Ctrl+`` |
| Clear Terminal | `Cmd+K` | `Ctrl+K` |

## Common Issues & Solutions

### Issue: "dotnet: command not found"

**Solution:**
- Restart your terminal/VSCode after installing .NET SDK
- macOS: Add to PATH in `~/.zshrc`: `export PATH="$PATH:$HOME/.dotnet"`
- Linux: Add to `~/.bashrc`: `export PATH="$PATH:$HOME/.dotnet"`

### Issue: C# extension not working

**Solution:**
1. Reload VSCode: `Cmd+Shift+P` > "Developer: Reload Window"
2. Check output: `View > Output > Select "C#" from dropdown
3. Restart OmniSharp: `Cmd+Shift+P` > "OmniSharp: Restart OmniSharp"

### Issue: No IntelliSense

**Solution:**
1. Wait for initial indexing (check bottom status bar)
2. Ensure `.csproj` files are valid
3. Run `dotnet restore`
4. Reload window

### Issue: Build errors

**Solution:**
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Issue: Can't debug/run application

**Solution:**
1. Check `.vscode/launch.json` exists
2. Ensure Web project is selected in debug dropdown
3. Try: `Cmd+Shift+P` > "Debug: Select and Start Debugging"

## VSCode Tips for C# Development

### 1. Use Code Snippets

Type these and press `Tab`:

- `class` - Create a class
- `ctor` - Create constructor
- `prop` - Create property
- `propfull` - Property with backing field
- `if` - If statement
- `for` - For loop
- `foreach` - Foreach loop

### 2. Organize Imports

Right-click in editor:
- "Organize Imports" - Remove unused, sort
- Or use `Shift+Option+O` (Mac) / `Shift+Alt+O` (Win/Linux)

### 3. Generate Code

Use `Cmd+.` (Mac) or `Ctrl+.` (Win/Linux) to:
- Generate constructors
- Implement interface
- Generate equals/hashcode
- Add using statements

### 4. Multi-File Search

- `Cmd+Shift+F` (Mac) or `Ctrl+Shift+F` (Win/Linux)
- Search across entire solution
- Use regex: Enable `.*` button

### 5. Integrated Terminal

- Open: `Ctrl+`` 
- Split: Click `+` icon
- Multiple terminals for different projects

### 6. Git Integration

VSCode has built-in Git support:
- Source Control: `Ctrl+Shift+G`
- Stage changes, commit, push
- View diffs inline

## Next Steps

1. Follow the main workshop guide: `CSharp-Northwind-Workshop.md`
2. Implement entities in the Core project
3. Create repositories in Infrastructure
4. Build Razor Pages in Web project
5. Run migrations: `dotnet ef migrations add InitialCreate`
6. Launch: Press `F5` and start coding!

## Useful VSCode Settings

Add to your User Settings (`Cmd+,` or `Ctrl+,`):

```json
{
  // C# specific
  "omnisharp.enableRoslynAnalyzers": true,
  "omnisharp.enableEditorConfigSupport": true,
  
  // Format on save
  "editor.formatOnSave": true,
  "[csharp]": {
    "editor.defaultFormatter": "ms-dotnettools.csharp"
  },
  
  // Auto save
  "files.autoSave": "afterDelay",
  "files.autoSaveDelay": 1000,
  
  // Breadcrumbs
  "breadcrumbs.enabled": true,
  
  // Terminal
  "terminal.integrated.fontSize": 14,
  
  // Explorer
  "explorer.confirmDelete": false,
  "explorer.confirmDragAndDrop": false
}
```

## Resources

- [VSCode C# Docs](https://code.visualstudio.com/docs/languages/csharp)
- [.NET CLI Reference](https://docs.microsoft.com/dotnet/core/tools/)
- [EF Core CLI](https://docs.microsoft.com/ef/core/cli/dotnet)
- [VSCode Keyboard Shortcuts](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-macos.pdf)

---

**You're ready to start the workshop! 🚀**

Open the main workshop file and start with Part 2: Creating Domain Entities.
