# CuaHangOto - Car Shop MVC Application
# STUDY CASE

An ASP.NET MVC application built with .NET Framework using traditional pages, demonstrating Role-Based Access Control (RBAC) implementation with custom RoleProvider.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Features](#features)
- [Installation](#installation)
- [Configuration](#configuration)
- [Role-Based Access Control (RBAC)](#role-based-access-control-rbac)
- [Project Structure](#project-structure)
- [Controllers](#controllers)
- [Database](#database)
- [License](#license)

## 🎯 Project Overview

CuaHangOto is an online car dealership management system that allows administrators and customers to:
- Browse vehicle inventory (Xe)
- Filter by manufacturer (Hang Xe)
- Organize by vehicle type (Dong Xe)
- Manage shopping carts (Gio Hang)
- Handle user accounts (Accounts)

## 🛠️ Technologies

- **Framework**: ASP.NET MVC 5 (.NET Framework)
- **Language**: C#
- **Database**: SQL Server (via DBML)
- **UI**: Razor Views (.cshtml)
- **Authentication**: Forms Authentication with Custom RoleProvider
- **Package Management**: NuGet

## 🏗️ Architecture

This application follows the **Model-View-Controller (MVC)** pattern:

### Model
- Represents application data and business logic
- Located in `/Models` folder
- Uses LINQ-to-SQL DataContext (CSDL.dbml)
- Includes custom RoleProvider for authentication

### View
- Renders the UI using Razor syntax (.cshtml)
- Located in `/Views` folder, organized by controller
- Uses shared layouts and master pages
- Includes CSS styling in `/Content/css`

### Controller
- Handles HTTP requests and routes
- Contains action methods that interact with models and return views
- Located in `/Controllers` folder
- Implements business logic routing

## ✨ Features

### User Management
- User registration (Signup)
- User login (Login)
- Role-based access control
- Custom RoleProvider implementation

### Product Management
- Vehicle catalog with filtering
- Manufacturer (Hang Xe) management
- Vehicle type (Dong Xe) categorization
- Detailed product views

### Shopping Cart
- Add/remove products
- Cart management
- Order processing

### Admin Panel
- Customer management
- Statistics and reporting
- Administrative dashboard

## 📦 Installation

### Prerequisites
- Visual Studio 2017 or later
- .NET Framework 4.7.2+
- SQL Server 2016 or later
- NuGet Package Manager

### Setup Steps

1. **Clone or download the repository**
   ```bash
   git clone <repository-url>
   ```

2. **Open the solution**
   ```bash
   # Open CuaHangOto.sln in Visual Studio
   ```

3. **Restore NuGet Packages**
   - Right-click the solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Or run in Package Manager Console:
   ```bash
   Update-Package -Reinstall
   ```

4. **Configure Database**
   - Update the connection string in `Web.config`
   - Run database migrations if applicable
   - The DBML file (CSDL.dbml) represents your database schema

5. **Build and Run**
   - Build the solution (Ctrl+Shift+B)
   - Press F5 to run the application
   - Navigate to `http://localhost:5000` (or configured port)

## ⚙️ Configuration

### Web.config
Key configuration sections:
```xml
<!-- Connection String -->
<connectionStrings>
    <add name="YourConnectionString" connectionString="..." />
</connectionStrings>

<!-- Authentication -->
<authentication mode="Forms">
    <forms loginUrl="~/Accounts/Login" timeout="30" />
</authentication>

<!-- Authorization -->
<authorization>
    <deny users="?" />
</authorization>
```

### App_Start Configuration
- **RouteConfig.cs**: URL routing rules
- **FilterConfig.cs**: Global action filters
- **BundleConfig.cs**: CSS/JS bundling
- **WebApiConfig.cs**: Web API configuration

## 🔐 Role-Based Access Control (RBAC)

### Overview
The application implements custom RBAC using a custom `RoleProvider` class to manage user roles and permissions.

### Implementation

#### 1. Custom RoleProvider
Located in `/Models/UserRoleProvider.cs`:
```csharp
public class UserRoleProvider : RoleProvider
{
    public override bool IsUserInRole(string username, string roleName)
    {
        // Check if user has the specified role
    }
    
    public override string[] GetRolesForUser(string username)
    {
        // Return all roles for a user
    }
    
    public override string[] GetUsersInRole(string roleName)
    {
        // Return all users in a role
    }
}
```

#### 2. Web.config Configuration
Configure the custom RoleProvider:
```xml
<system.web>
    <roleManager enabled="true" defaultProvider="CustomRoleProvider">
        <providers>
            <clear/>
            <add name="CustomRoleProvider" 
                 type="CuaHangOto.Models.UserRoleProvider" />
        </providers>
    </roleManager>
</system.web>
```

#### 3. Protecting Controllers/Actions
Use `Authorize` and `Authorize(Roles = "...")` attributes:

```csharp
// Require authentication for all actions
[Authorize]
public class AdminController : Controller
{
    // Only Admin role can access this action
    [Authorize(Roles = "Admin")]
    public ActionResult Dashboard()
    {
        return View();
    }
}

// Restrict to multiple roles
[Authorize(Roles = "Admin,Manager")]
public ActionResult ManageUsers()
{
    return View();
}

// Anonymous access allowed
public class HomeController : Controller
{
    [AllowAnonymous]
    public ActionResult Index()
    {
        return View();
    }
}
```

#### 4. Checking Roles Programmatically
In your controller or view:
```csharp
if (User.IsInRole("Admin"))
{
    // Show admin options
}

// Get all roles for current user
var userRoles = Roles.GetRolesForUser(User.Identity.Name);
```

#### 5. In Views (Razor)
```html
@if (User.IsInRole("Admin"))
{
    <a href="@Url.Action("Dashboard", "Admin")">Admin Panel</a>
}

@if (User.Identity.IsAuthenticated)
{
    <p>Welcome, @User.Identity.Name</p>
}
```

### Common Roles in This Application
- **Admin**: Full system access, user management, statistics
- **User/Customer**: Browse products, manage cart, place orders
- **Guest**: View products, no cart/order functionality

## 📁 Project Structure

```
CuaHangOto/
├── App_Start/                 # Application configuration
│   ├── BundleConfig.cs
│   ├── FilterConfig.cs
│   ├── RouteConfig.cs
│   └── WebApiConfig.cs
├── Content/                   # Static CSS
│   ├── css/
│   │   ├── CreateForm.css
│   │   ├── db.css
│   │   ├── detail.css
│   │   ├── global.css
│   │   └── style.css
│   └── PagedList.css
├── Controllers/               # MVC Controllers
│   ├── AccountsController.cs
│   ├── AdminController.cs
│   ├── DongXeController.cs    # Vehicle Type
│   ├── GioHangController.cs   # Shopping Cart
│   ├── HangXeController.cs    # Manufacturer
│   ├── HomeController.cs
│   └── XeController.cs        # Vehicle/Product
├── HinhAnh/                   # Images
│   └── images/
├── Models/                    # Data models & business logic
│   ├── CSDL.dbml             # Database schema
│   ├── CSDL.designer.cs
│   ├── GioHang.cs            # Shopping Cart model
│   └── UserRoleProvider.cs   # Custom RoleProvider
├── Views/                     # Razor views
│   ├── Shared/
│   │   ├── _Layout.cshtml    # Master layout
│   │   └── Error.cshtml
│   ├── Accounts/             # Authentication views
│   ├── Admin/                # Admin panel views
│   ├── DongXe/               # Vehicle type views
│   ├── GioHang/              # Shopping cart views
│   ├── HangXe/               # Manufacturer views
│   ├── Home/                 # Home page views
│   ├── Xe/                   # Product views
│   └── _ViewStart.cshtml
├── Properties/
│   └── AssemblyInfo.cs
├── CuaHangOto.csproj
├── CuaHangOto.sln
├── Global.asax
├── Web.config
└── packages.config
```

## 🎮 Controllers

### HomeController
- `Index()`: Display home page
- `About()`: About page
- `Contact()`: Contact page

### XeController
- `SanPham()`: List all products
- `Detail(int id)`: Product details
- `Create()`, `Edit()`, `Delete()`: Admin product management

### HangXeController
- `HangXe()`: List manufacturers
- `Detail()`, `Create()`, `Edit()`, `Delete()`: Manufacturer management

### DongXeController
- `DongXe()`: List vehicle types
- Management actions for admins

### GioHangController
- `GioHang()`: View shopping cart
- Add/remove items

### AccountsController
- `Login()`: User login
- `Signup()`: User registration

### AdminController
- `Index()`: Admin dashboard
- `KhachHang()`: Customer management
- `ThongKe()`: Statistics

## 💾 Database

The application uses **LINQ-to-SQL** with a DBML (DataSet Markup Language) file for database mapping:
- **File**: `Models/CSDL.dbml`
- **Auto-generated**: `Models/CSDL.designer.cs`

### Key Entities
- **Users**: User accounts and credentials
- **Roles**: User roles (Admin, User, etc.)
- **Xe** (Vehicles): Product catalog
- **HangXe** (Manufacturers): Vehicle manufacturers
- **DongXe** (Vehicle Types): Vehicle classifications
- **GioHang** (Shopping Cart): Cart items and orders

## 🚀 Development Workflow

1. **Create/modify models** in `/Models`
2. **Add/update database** entities via DBML designer
3. **Create controllers** with CRUD operations
4. **Design views** in `/Views` using Razor syntax
5. **Style with CSS** from `/Content/css`
6. **Implement authorization** using Authorize attributes
7. **Test thoroughly** before deployment

## 📝 Code Standards

- Follow Microsoft C# naming conventions
- Use meaningful variable and method names
- Add XML documentation comments to public methods
- Keep controller actions focused and single-purpose
- Use dependency injection where applicable
- Validate user input on both client and server

## 🔒 Security Best Practices

1. **Authentication**: Use Forms Authentication with secure password hashing
2. **Authorization**: Always validate user roles on the server
3. **CSRF Protection**: Use `@Html.AntiForgeryToken()` in forms
4. **SQL Injection**: Use parameterized queries (LINQ-to-SQL handles this)
5. **XSS Prevention**: HTML-encode output with Razor `@` syntax
6. **Sensitive Data**: Store secrets in `Web.config` or secure configuration

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Support

For issues or questions, please open an issue on the repository or contact the project maintainers.

---

**Last Updated**: May 2026  
**Framework**: ASP.NET MVC 5 (.NET Framework)  
**Status**: Active Development
