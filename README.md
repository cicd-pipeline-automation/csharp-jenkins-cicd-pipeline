
# README.md — Complete CI/CD Documentation for Sample Flask Login (C# ASP.NET Core)

## 📌 Overview
This project is a **C# ASP.NET Core MVC Login Application**, packaged into a **Windows Installer (.exe)** using **Inno Setup**, and built/deployed through a fully automated **Jenkins CI/CD Pipeline**.

The pipeline:
- Builds the .NET application  
- Publishes artifacts  
- Generates installer configuration  
- Compiles the Windows installer  
- Automatically bumps the version (Alpha, Beta, Patch, Minor, Major)  
- Stores the installer in the `result/` folder  
- Emails the `.exe` installer  

---

# 📁 0. Project Folder Structure (Complete)

```
Sample-Flask-Login/
├── Jenkinsfile
├── README.md
├── version.txt
├── installer/
│   ├── installer_script.iss
│   ├── build_config.iss (auto-generated)
│   ├── icons/
│   │   └── app.ico
│   └── assets/
│       ├── banner.bmp
│       └── license.txt
├── config/
│   ├── appsettings.json
│   ├── appsettings.Dev.json
│   ├── appsettings.QA.json
│   └── appsettings.Prod.json
├── src/
│   ├── SampleFlaskLogin.sln
│   └── SampleFlaskLogin/
│       ├── Program.cs
│       ├── Startup.cs
│       ├── SampleFlaskLogin.csproj
│       ├── Controllers/
│       │   ├── HomeController.cs
│       │   └── AccountController.cs
│       ├── Models/
│       │   └── LoginViewModel.cs
│       ├── Services/
│       │   ├── IUserService.cs
│       │   └── InMemoryUserService.cs
│       ├── Views/
│       │   ├── _ViewImports.cshtml
│       │   ├── _ViewStart.cshtml
│       │   ├── Shared/_Layout.cshtml
│       │   ├── Home/Index.cshtml
│       │   └── Account/
│       │       ├── Login.cshtml
│       │       └── AccessDenied.cshtml
│       └── wwwroot/
│           └── css/
│               └── site.css
├── publish/ (auto-generated)
└── result/  (generated installer .exe)
```

---

# 1. Software Installation Steps
(…FULL CONTENT MAINTAINED…)
...