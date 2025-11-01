# Tailwind CSS Setup Guide for ASP.NET Core

This guide will help you set up Tailwind CSS in your Northwind Workshop project.

## Why Tailwind CSS?

Tailwind CSS is a utility-first CSS framework that provides low-level utility classes to build custom designs. It matches perfectly with the Next.js version of this workshop and provides:

- **Rapid Development** - Build UI quickly with utility classes
- **Consistency** - Design system built-in
- **Responsive** - Mobile-first approach
- **Customizable** - Easily extend and configure
- **Modern** - Matches the Next.js workshop styling

## Prerequisites

Before setting up Tailwind, ensure you have:
- Node.js installed (version 22 or higher)
- npm or yarn package manager
- Your Northwind Workshop project created

## Step-by-Step Setup

### 1. Install Node.js (if not already installed)

**macOS:**
```bash
brew install node
```

**Linux (Ubuntu/Debian):**
```bash
sudo apt update
sudo apt install nodejs npm
```

**Windows:**
Download from [nodejs.org](https://nodejs.org)

**Verify installation:**
```bash
node --version
npm --version
```

### 2. Initialize npm in Your Web Project

```bash
cd NorthwindWorkshop.Web

# Create package.json
npm init -y
```

### 3. Install Tailwind CSS

```bash
# Install Tailwind CSS v4 (stable release)
npm install tailwindcss @tailwindcss/cli

# Note: Tailwind 4 uses CSS-first configuration (no tailwind.config.js needed!)
```

Tailwind CSS 4 is a major rewrite that's **CSS-first** - configuration is done directly in CSS using `@import` and `@theme` instead of JavaScript files.

### 4. Create CSS Input File

Create `wwwroot/css/app.css`:

```css
/* Import Tailwind's base styles */
@import "tailwindcss";

/* Configure theme (replaces tailwind.config.js) */
@theme {
  /* Customize colors */
  --color-primary: #3b82f6;
  --color-secondary: #6b7280;
  
  /* Add custom spacing if needed */
  /* --spacing-18: 4.5rem; */
}

/* Configure content paths (what files to scan) */
@source "../../Pages/**/*.cshtml";
@source "../../Views/**/*.cshtml";

/* Custom Component Classes */
@layer components {
  /* Button Styles */
  .btn-primary {
    @apply bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors duration-200 font-medium shadow-sm;
  }
  
  .btn-secondary {
    @apply bg-gray-600 text-white px-4 py-2 rounded-lg hover:bg-gray-700 transition-colors duration-200 font-medium shadow-sm;
  }
  
  .btn-success {
    @apply bg-green-600 text-white px-4 py-2 rounded-lg hover:bg-green-700 transition-colors duration-200 font-medium shadow-sm;
  }
  
  .btn-danger {
    @apply bg-red-600 text-white px-4 py-2 rounded-lg hover:bg-red-700 transition-colors duration-200 font-medium shadow-sm;
  }
  
  .btn-outline {
    @apply border-2 border-gray-300 text-gray-700 px-4 py-2 rounded-lg hover:bg-gray-50 transition-colors duration-200 font-medium;
  }
  
  /* Card Styles */
  .card {
    @apply bg-white rounded-lg shadow-md overflow-hidden;
  }
  
  .card-hover {
    @apply bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-200 overflow-hidden;
  }
  
  /* Badge Styles */
  .badge {
    @apply inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium;
  }
  
  .badge-primary {
    @apply bg-blue-100 text-blue-800;
  }
  
  .badge-success {
    @apply bg-green-100 text-green-800;
  }
  
  .badge-warning {
    @apply bg-yellow-100 text-yellow-800;
  }
  
  .badge-danger {
    @apply bg-red-100 text-red-800;
  }
  
  .badge-info {
    @apply bg-cyan-100 text-cyan-800;
  }
  
  .badge-gray {
    @apply bg-gray-100 text-gray-800;
  }
  
  /* Input Styles */
  .input {
    @apply w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent transition-all;
  }
  
  .input-error {
    @apply w-full px-4 py-2 border-2 border-red-300 rounded-lg focus:ring-2 focus:ring-red-500 focus:border-transparent;
  }
  
  /* Select Styles */
  .select {
    @apply w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent bg-white;
  }
  
  /* Table Styles */
  .table-row-hover {
    @apply hover:bg-gray-50 transition-colors duration-150;
  }
}

/* Custom Utilities */
@layer utilities {
  .text-balance {
    text-wrap: balance;
  }
}
```

### 5. Configure Build Scripts

Edit `package.json` and add these scripts:

```json
{
  "name": "northwindworkshop.web",
  "version": "1.0.0",
  "description": "Northwind Workshop Web Application",
  "scripts": {
    "css:build": "tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/output.css --minify",
    "css:watch": "tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/output.css --watch",
    "css:dev": "npm run css:watch"
  },
  "keywords": [],
  "author": "",
  "license": "ISC",
  "devDependencies": {
    "@tailwindcss/cli": "^4.0.0",
    "tailwindcss": "^4.0.0"
  }
}
```

### 6. Build Tailwind CSS

**For Development (with watch mode):**
```bash
npm run css:watch
```

This will watch for changes and rebuild automatically. Keep this running in a terminal while developing.

**For Production:**
```bash
npm run css:build
```

This creates a minified `output.css` file.

### 9. Include CSS in Your Layout

Update `Pages/Shared/_Layout.cshtml`:

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Northwind Traders</title>
    
    <!-- Tailwind CSS -->
    <link rel="stylesheet" href="~/css/output.css" asp-append-version="true" />
</head>
<body class="bg-gray-50">
    @RenderBody()
    
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### 10. Update .gitignore

Add these lines to your `.gitignore`:

```
# Node modules
node_modules/

# Tailwind output
wwwroot/css/output.css
wwwroot/css/output.css.map

# npm
package-lock.json
```

## Development Workflow

### Option 1: Run Both (Recommended)

**Terminal 1 - Watch Tailwind:**
```bash
cd NorthwindWorkshop.Web
npm run css:watch
```

**Terminal 2 - Run App:**
```bash
cd NorthwindWorkshop.Web
dotnet watch run
```

### Option 2: VSCode Tasks

Create `.vscode/tasks.json`:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "watch-tailwind",
      "type": "npm",
      "script": "css:watch",
      "path": "NorthwindWorkshop.Web/",
      "problemMatcher": [],
      "isBackground": true,
      "presentation": {
        "group": "watchers"
      }
    },
    {
      "label": "watch-dotnet",
      "type": "process",
      "command": "dotnet",
      "args": ["watch", "run"],
      "options": {
        "cwd": "${workspaceFolder}/NorthwindWorkshop.Web"
      },
      "problemMatcher": "$msCompile",
      "isBackground": true,
      "presentation": {
        "group": "watchers"
      }
    },
    {
      "label": "dev",
      "dependsOn": ["watch-tailwind", "watch-dotnet"],
      "problemMatcher": []
    }
  ]
}
```

Now run: `Cmd+Shift+P` > "Tasks: Run Task" > "dev"

## Tailwind Utility Classes Reference

### Layout
```
container, mx-auto, flex, grid, block, inline-block, hidden
```

### Spacing
```
p-4 (padding), m-4 (margin), px-4, py-4, mt-4, mb-4, ml-4, mr-4
gap-4, space-x-4, space-y-4
```

### Sizing
```
w-full, w-64, h-64, min-h-screen, max-w-7xl
```

### Typography
```
text-sm, text-base, text-lg, text-xl, text-2xl, text-3xl
font-normal, font-medium, font-semibold, font-bold
text-gray-900, text-blue-600, text-center, text-left, text-right
```

### Colors
```
bg-white, bg-gray-50, bg-blue-600
text-gray-900, text-white, text-blue-600
border-gray-300, border-blue-500
```

### Borders & Shadows
```
border, border-2, rounded, rounded-lg, rounded-full
shadow, shadow-md, shadow-lg
```

### Flexbox
```
flex, flex-col, flex-row, justify-center, justify-between
items-center, items-start, items-end, gap-4
```

### Grid
```
grid, grid-cols-1, grid-cols-2, grid-cols-3, grid-cols-4
gap-4, col-span-2
```

### Responsive Design
```
sm:text-lg (≥640px), md:flex (≥768px), lg:grid-cols-3 (≥1024px)
xl:max-w-7xl (≥1280px), 2xl:text-4xl (≥1536px)
```

### Hover & Focus States
```
hover:bg-blue-700, hover:text-white
focus:ring-2, focus:ring-blue-500, focus:outline-none
```

### Transitions
```
transition, transition-colors, transition-all
duration-200, duration-300, ease-in-out
```

## Common Patterns

### Form Input
```html
<input type="text" 
       class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
       placeholder="Enter text...">
```

### Button
```html
<button class="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors">
    Click Me
</button>
```

### Card
```html
<div class="bg-white rounded-lg shadow-md p-6">
    <h3 class="text-lg font-semibold mb-2">Card Title</h3>
    <p class="text-gray-600">Card content goes here.</p>
</div>
```

### Badge
```html
<span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
    Active
</span>
```

### Table
```html
<table class="min-w-full divide-y divide-gray-200">
    <thead class="bg-gray-50">
        <tr>
            <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Name
            </th>
        </tr>
    </thead>
    <tbody class="bg-white divide-y divide-gray-200">
        <tr class="hover:bg-gray-50">
            <td class="px-6 py-4 whitespace-nowrap">
                <div class="text-sm text-gray-900">John Doe</div>
            </td>
        </tr>
    </tbody>
</table>
```

## Troubleshooting

### Classes not applying
1. Ensure `npm run css:watch` is running
2. Check `tailwind.config.js` content paths
3. Verify `output.css` exists in `wwwroot/css/`
4. Hard refresh browser (Cmd+Shift+R / Ctrl+Shift+F5)

### Build errors
```bash
# Clear node modules and reinstall
rm -rf node_modules package-lock.json
npm install
```

### CSS not updating
1. Stop `css:watch`
2. Delete `wwwroot/css/output.css`
3. Run `npm run css:build`
4. Restart watch mode

## VSCode Extensions for Tailwind

Install these VSCode extensions for better Tailwind development:

1. **Tailwind CSS IntelliSense** (bradlc.vscode-tailwindcss)
   - Autocomplete for Tailwind classes
   - Hover preview of CSS
   - Linting

2. **PostCSS Language Support** (csstools.postcss)
   - Syntax highlighting for `@tailwind` directives

3. **Headwind** (heybourn.headwind)
   - Automatically sort Tailwind classes

## Production Build

Before deploying, create a production build:

```bash
npm run css:build
```

This creates a minified CSS file optimized for production.

## Additional Resources

- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
- [Tailwind CSS Cheat Sheet](https://nerdcave.com/tailwind-cheat-sheet)
- [Tailwind UI Components](https://tailwindui.com)
- [Heroicons (Free Icons)](https://heroicons.com)

## Next Steps

1. Build custom components using `@layer components`
2. Extend the theme in `tailwind.config.js`
3. Add Tailwind plugins (forms, typography, etc.)
4. Explore responsive design with breakpoints
5. Create a design system for your app

---

**You're ready to use Tailwind CSS in your Northwind Workshop! 🎨**
