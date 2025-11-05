# NorthwindWorkshop.Web

**ASP.NET Core 9.0 Razor Pages Web Application**

This is the **Presentation Layer** of the Northwind Workshop application, built using ASP.NET Core Razor Pages with Clean Architecture principles. It provides a modern web interface for managing and viewing Northwind database entities.

## 🎯 Project Overview

The web project serves as the user interface for the Northwind Workshop application, demonstrating:

- **ASP.NET Core 9.0** with Razor Pages
- **Clean Architecture** presentation layer
- **Repository Pattern** integration via Dependency Injection
- **Tailwind CSS 4** for modern, responsive styling
- **Entity Framework Core** integration for data display

## 🏗️ Architecture Role

This project represents the **Presentation Layer** in the Clean Architecture pattern:

```
┌─────────────────────────────────────┐
│        NorthwindWorkshop.Web        │  ← This Project
│         (Presentation Layer)        │
├─────────────────────────────────────┤
│   NorthwindWorkshop.Infrastructure  │
│        (Data Access Layer)          │
├─────────────────────────────────────┤
│      NorthwindWorkshop.Core         │
│         (Domain Layer)              │
└─────────────────────────────────────┘
```

## 📁 Project Structure

```
NorthwindWorkshop.Web/
├── Pages/                          # Razor Pages (Views + Page Models)
│   ├── Categories/                 # Category management pages
│   │   ├── Index.cshtml           # Category list view
│   │   └── Index.cshtml.cs        # Category list page model
│   ├── Customers/                 # Customer management pages
│   │   ├── Index.cshtml           # Customer list view
│   │   └── Index.cshtml.cs        # Customer list page model
│   ├── Products/                  # Product management pages
│   │   ├── Index.cshtml           # Product list view
│   │   └── Index.cshtml.cs        # Product list page model
│   ├── Shared/                    # Shared layout components
│   │   ├── _Layout.cshtml         # Main application layout
│   │   ├── _Sidebar.cshtml        # Navigation sidebar
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Index.cshtml               # Home page
│   ├── Error.cshtml               # Error page
│   ├── _ViewImports.cshtml        # Global using statements
│   └── _ViewStart.cshtml          # Global layout settings
├── ViewModels/                    # Data Transfer Objects for views
│   ├── CategoryListViewModel.cs   # Category list display model
│   ├── CustomerListViewModel.cs   # Customer list display model
│   └── ProductListViewModel.cs    # Product list display model
├── wwwroot/                       # Static web assets
│   ├── css/                       # Stylesheets (Tailwind CSS output)
│   ├── js/                        # JavaScript files
│   └── lib/                       # Client-side libraries
├── Program.cs                     # Application startup & configuration
├── appsettings.json              # Application configuration
├── appsettings.Development.json  # Development-specific settings
├── package.json                  # Node.js dependencies (Tailwind CSS)
└── northwind.db                  # SQLite database file
```

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- Node.js 22+ (for Tailwind CSS)

### Running the Application

1. **Navigate to the Web project directory:**
   ```bash
   cd NorthwindWorkshop.Web
   ```

2. **Install Node.js dependencies:**
   ```bash
   npm install
   ```

3. **Build Tailwind CSS (one-time):**
   ```bash
   npm run css:build
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

   Or with hot reload:
   ```bash
   dotnet watch run
   ```

5. **Development with CSS watching (optional):**
   ```bash
   # In a separate terminal
   npm run css:watch
   ```

### Access the Application

- **Local URL:** `https://localhost:7067` (or check console output)
- **Database:** Automatically created and seeded on first run

## 🎨 Styling & UI

### Tailwind CSS 4

The project uses **Tailwind CSS 4** for styling:

- **Source:** `wwwroot/css/app.css`
- **Output:** `wwwroot/css/output.css`
- **Build:** `npm run css:build`
- **Watch:** `npm run css:watch`

### Design System

- **Colors:** Blue primary, gray neutral palette
- **Components:** Custom badges, buttons, cards, and form elements
- **Layout:** Responsive grid system with sidebar navigation
- **Typography:** Clean, readable fonts with proper hierarchy

### Custom CSS Classes

```css
/* Utility classes defined in app.css */
.btn-primary         /* Primary action button */
.badge-primary       /* Primary status badge */
.badge-success       /* Success status badge */
.badge-warning       /* Warning status badge */
.badge-danger        /* Error status badge */
.card                /* Container card component */
```

## 📄 Pages & Features

### 🏠 Home Page (`/`)
- **File:** `Pages/Index.cshtml`
- **Features:** Dashboard overview, navigation links

### 📦 Products (`/Products`)
- **Files:** `Pages/Products/Index.cshtml[.cs]`
- **Features:**
  - Product listing with pagination
  - Search by product name
  - Filter by low stock, discontinued status
  - Display with category and supplier information

### 🏷️ Categories (`/Categories`)
- **Files:** `Pages/Categories/Index.cshtml[.cs]`
- **Features:**
  - Category listing with product counts
  - Search by name and description
  - Filter for empty categories
  - Status indicators (Well Stocked, Low Inventory, Empty)

### 👥 Customers (`/Customers`)
- **Files:** `Pages/Customers/Index.cshtml[.cs]`
- **Features:**
  - Customer directory
  - Search and filtering capabilities
  - Contact information display

### Navigation
- **File:** `Pages/Shared/_Sidebar.cshtml`
- **Features:** Responsive sidebar with active state highlighting

## 🔧 Configuration

### Database Connection

**File:** `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=northwind.db"
  }
}
```

### Dependency Injection

**File:** `Program.cs`
```csharp
// Repository registrations
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

## 🎯 Key Patterns & Practices

### 1. **MVVM Pattern**
```csharp
// Page Model (Controller logic)
public class IndexModel : PageModel
{
    public async Task OnGetAsync() { /* Load data */ }
}

// View Model (Data shape)
public class ProductListViewModel
{
    public string ProductName { get; set; }
    // ... other display properties
}
```

### 2. **Repository Pattern Integration**
```csharp
public class IndexModel : PageModel
{
    private readonly IProductRepository _repository;

    public IndexModel(IProductRepository repository)
    {
        _repository = repository;  // Dependency Injection
    }
}
```

### 3. **Search & Filtering**
```csharp
// URL: /Products?searchTerm=chai&showDiscontinued=true
public async Task OnGetAsync(string? searchTerm, bool showDiscontinued = false)
{
    // Apply filters and search logic
}
```

## 🛠️ Development Workflow

### 1. **Adding New Pages**

1. Create page directory: `Pages/NewEntity/`
2. Add Razor page: `Index.cshtml` and `Index.cshtml.cs`
3. Create ViewModel: `ViewModels/NewEntityListViewModel.cs`
4. Update navigation: `Pages/Shared/_Sidebar.cshtml`

### 2. **Modifying Styles**

1. Edit source CSS: `wwwroot/css/app.css`
2. Rebuild CSS: `npm run css:build`
3. For development: `npm run css:watch`

### 3. **Database Changes**

When the Core/Infrastructure layers change:
```bash
dotnet ef database update --project ../NorthwindWorkshop.Infrastructure --startup-project .
```

## 🔍 Debugging & Development

### Hot Reload
```bash
dotnet watch run
# Changes to .cs and .cshtml files trigger automatic reload
```

### CSS Development
```bash
npm run css:watch
# Watches for changes to Tailwind classes in .cshtml files
```

### Database Inspection
- **File:** `northwind.db` (SQLite database)
- **Tools:** DB Browser for SQLite, Azure Data Studio, or VS Code extensions

## 📊 Performance Considerations

### 1. **Database Queries**
- Repository methods use eager loading (`Include()`) to prevent N+1 queries
- ViewModels project only needed data to reduce memory usage

### 2. **CSS Optimization**
- Tailwind CSS purges unused styles in production
- Minified output for smaller file sizes

### 3. **Caching Opportunities**
- Static assets cached by browser
- Consider output caching for list pages
- Repository-level caching for frequently accessed data

## 🧪 Testing

### Manual Testing Checklist

- [ ] Home page loads without errors
- [ ] All navigation links work correctly
- [ ] Product search and filtering functions
- [ ] Category listing displays correctly
- [ ] Customer directory is accessible
- [ ] Responsive design works on mobile devices
- [ ] Database is created and seeded automatically

### Automated Testing (Future)

Recommended test structure:
```
NorthwindWorkshop.Web.Tests/
├── Pages/                    # Page model unit tests
├── ViewModels/              # ViewModel tests
└── Integration/             # Full integration tests
```

## 🔐 Security Notes

### Input Validation
- Model binding provides basic protection
- Search terms are parameterized (SQL injection safe)
- ViewModels prevent over-posting attacks

### Authentication (Not Implemented)
This is a demonstration project without authentication. For production:
- Add ASP.NET Core Identity
- Implement authorization policies
- Secure sensitive operations

## 🚀 Production Deployment

### Build for Production
```bash
# Restore packages
dotnet restore

# Build optimized CSS
npm run css:build

# Build application
dotnet build --configuration Release

# Publish application
dotnet publish --configuration Release --output ./publish
```

### Environment Configuration
- Update `appsettings.Production.json` for production settings
- Use proper database connection string
- Enable HTTPS enforcement
- Configure logging appropriately

## 📚 Related Documentation

- **Main Project:** `../README.md` - Overall project documentation
- **Architecture:** `../docs/CSharp-Northwind-Workshop.md` - Detailed tutorial
- **Changes:** `../docs/changes/` - Implementation change logs
- **Setup:** `../docs/VSCode-QuickStart-Guide.md` - Development environment setup

## 🤝 Contributing

When adding new features to the web project:

1. Follow the existing Razor Pages pattern
2. Create appropriate ViewModels for data transfer
3. Use Tailwind CSS classes for styling consistency
4. Add navigation links to `_Sidebar.cshtml`
5. Test responsive design on multiple screen sizes
6. Document any new configuration requirements

## 💡 Tips for Development

1. **Use CSS watching** during development: `npm run css:watch`
2. **Hot reload** speeds up testing: `dotnet watch run`
3. **ViewModels** keep views clean and performant
4. **Repository injection** enables easy testing and mocking
5. **Consistent styling** using Tailwind utility classes
6. **Browser dev tools** help debug responsive layouts

---

**Happy coding!** 🎉

This web project demonstrates modern ASP.NET Core development practices with Clean Architecture, responsive design, and excellent user experience.