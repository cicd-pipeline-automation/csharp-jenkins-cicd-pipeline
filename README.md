# 📘 **README.md — Complete CI/CD Documentation for Sample Flask Login (C# ASP.NET Core)**
### *Windows Installer Build using Inno Setup, Automated via Jenkins Pipeline*

# 📌 Project Overview
This project is a **C# ASP.NET Core MVC Login Application** packaged into a **Windows Installer (.exe)** using **Inno Setup**, and fully automated using a **Jenkins CI/CD Pipeline**.

### ✔ Major Features
- ASP.NET Core MVC Login Application  
- Secure Cookie Authentication  
- Multi-environment configuration (Dev, QA, Prod)  
- Inno Setup packaging → Generates **Windows Installer (.exe)**  
- Jenkins CI/CD with parameters  
- Automatic version bumping  
- Build artifacts emailed automatically  
- Production-ready folder structure  

# 📁 Folder Structure (Production Ready)
```
SampleFlaskLogin/
├── src/
│   └── SampleFlaskLogin/
│       ├── Controllers/
│       ├── Models/
│       ├── Services/
│       ├── Views/
│       ├── wwwroot/
│       ├── Program.cs
│       ├── Startup.cs
│       ├── SampleFlaskLogin.csproj
│       └── appsettings.json
│
├── installer/
│   ├── installer_script.iss
│   ├── build_config.iss
│   ├── icons/app.ico
│   └── assets/banner.bmp
│
├── scripts/
│   ├── pre_build.sh
│   ├── post_build.sh
│   └── send_email.ps1
│
├── Jenkinsfile
├── version.txt
└── README.md
```

# 1️⃣ SOFTWARE INSTALLATION STEPS (ALL PREREQUISITES)

## 1.1 Install .NET SDK 6.0+
Download: https://dotnet.microsoft.com/en-us/download/dotnet/6.0  
Verify:
```
dotnet --info
```

## 1.2 Install Inno Setup Compiler
Download: https://jrsoftware.org/isdl.php  
Verify:
```
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" /?
```

## 1.3 Install Jenkins
Required Plugins:
- Pipeline  
- Git  
- Email Extension Plugin  
- Credentials Plugin  

## 1.4 Install Git
Verify:
```
git --version
```

## 1.5 Install Java (Required by Jenkins)
Verify:
```
java -version
```

## 1.6 Jenkins Windows Build Agent Requirements
| Dependency | Required |
|-----------|----------|
| .NET 6 SDK | ✔ |
| Inno Setup 6 | ✔ |
| Git | ✔ |
| SMTP Access | Optional |

# 2️⃣ PIPELINE DETAILED EXPLANATION

## Stage 1 — Checkout  
## Stage 2 — Read & Bump Version  
Versioning rules included.

## Stage 3 — Restore & Build (.NET)  
## Stage 4 — Prepare Folders  
## Stage 5 — Generate Build Config  
## Stage 6 — Run Inno Setup Compiler  
## Stage 7 — Archive Artifact  
## Stage 8 — Send Email  

# 3️⃣ STEP-BY-STEP JENKINS CREDENTIALS SETUP
Includes SMTP, GitHub, and Jenkinsfile usage.

# 4️⃣ PIPELINE EXECUTION PLAN — END TO END
Summary of all steps executed inside Jenkins.

# 🎉 Documentation Complete
