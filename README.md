
# 📘 README.md — Complete CI/CD Documentation for Sample Flask Login (C# ASP.NET Core)
### *Windows Installer Build using Inno Setup, Automated via Jenkins Pipeline*

---

# 📌 Project Overview

This project is a **C# ASP.NET Core MVC Login Application** packaged into a **Windows Installer (.exe)** using **Inno Setup**, and fully automated using a **Jenkins CI/CD Pipeline**.

### ✔ Major Features

- ASP.NET Core MVC login application  
- Secure Cookie Authentication  
- Multi-environment configuration (Dev, QA, Prod)  
- Inno Setup packaging → Generates **Windows Installer (.exe)**  
- Jenkins CI/CD pipeline with parameters  
- Automatic version bumping (Alpha / Beta / Patch / Minor / Major)  
- Build artifacts stored & emailed  
- Production-ready folder structure  

---

# 📁 Folder Structure (Production Ready)

```
SampleFlaskLogin/
├── src/
│   └── SampleFlaskLogin/
│       ├── Controllers/
│       │   ├── AccountController.cs
│       │   └── HomeController.cs
│       ├── Models/
│       │   └── LoginViewModel.cs
│       ├── Services/
│       │   ├── IUserService.cs
│       │   └── InMemoryUserService.cs
│       ├── Views/
│       │   ├── Account/
│       │   │   ├── Login.cshtml
│       │   │   └── AccessDenied.cshtml
│       │   ├── Home/
│       │   │   └── Index.cshtml
│       │   ├── Shared/
│       │   │   └── _Layout.cshtml
│       │   ├── _ViewImports.cshtml
│       │   └── _ViewStart.cshtml
│       ├── wwwroot/
│       │   └── css/site.css
│       ├── Program.cs
│       ├── Startup.cs
│       ├── SampleFlaskLogin.csproj
│       └── appsettings.json
│
├── installer/
│   ├── installer_script.iss
│   ├── build_config.iss   (generated during pipeline)
│   ├── icons/
│   │   └── app.ico
│   └── assets/
│       └── banner.bmp
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

---

# ---------------------------------------------------
# 1️⃣ SOFTWARE INSTALLATION STEPS (ALL PREREQUISITES)
# ---------------------------------------------------

This section covers everything required to build, run, package, and deploy the project.

---

## **1.1 Install .NET SDK 6.0+**

Install on:

- Jenkins Windows Build Agent  
- Developer machines  

Download:  
https://dotnet.microsoft.com/en-us/download/dotnet/6.0  

Verify:

```
dotnet --info
```

---

## **1.2 Install Inno Setup Compiler (ISCC.exe)**

Download:  
https://jrsoftware.org/isdl.php  

Install default path:

```
C:\Program Files (x86)\Inno Setup 6```

Verify:

```
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" /?
```

---

## **1.3 Install Jenkins**

Download Jenkins LTS:  
https://www.jenkins.io/download/

### Required Plugins

| Plugin | Purpose |
|--------|---------|
| Pipeline | Execute Jenkinsfile |
| Git | Checkout source code |
| Email Extension Plugin | Send build emails |
| Credentials Plugin | Store secrets |
| NodeLabel Plugin | Optional agent selection |
| AnsiColor Plugin | Optional console colors |

The Jenkins service account must have:

- Access to Inno Setup installation  
- Permission to run `.exe` and `.bat`  

---

## **1.4 Install Git**

Verify:

```
git --version
```

---

## **1.5 Install Java (Required for Jenkins)**

Verify:

```
java -version
```

---

## **1.6 Windows Build Agent Requirements**

| Dependency | Required | Purpose |
|-----------|----------|---------|
| .NET 6 SDK | ✔ | Build application |
| Inno Setup 6 | ✔ | Compile installer |
| Git | ✔ | Pull from repository |
| SMTP Access | Optional | For email |
| VS Build Tools | Optional | MSBuild support |

---

# ----------------------------------------------------
# 2️⃣ PIPELINE DETAILED EXPLANATION — STAGE BY STAGE
# ----------------------------------------------------

## **Stage 1 — Checkout**

Pulls repository from Git:

```
checkout scm
```

---

## **Stage 2 — Read & Bump Version**

Reads the version from `version.txt`.

### Versioning Rules

| TYPE | Rule | Example | Result |
|------|------|---------|--------|
| Alpha | No numeric bump | 1.0.0 | v1.0.0_Alpha |
| Beta | No numeric bump | 1.0.0 | v1.0.0_Beta |
| Patch | +1 patch | 1.0.0 | v1.0.1_Patch |
| Minor | +1 minor, reset patch | 1.0.0 | v1.1.0_Minor |
| Major | +1 major, reset others | 1.0.0 | v2.0.0_Major |

Installer name format:

```
result/v1.1.0_Minor.exe
```

---

## **Stage 3 — Restore & Build (.NET)**

```
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish/
```

---

## **Stage 4 — Prepare Folders**

Creates:

```
result/
```

---

## **Stage 5 — Generate Build Config**

Pipeline dynamically creates:

```
installer/build_config.iss
```

---

## **Stage 6 — Run Inno Setup Compiler**

```
ISCC.exe installer/build_config.iss
```

Output:

```
result/MyApp.exe
```

---

## **Stage 7 — Archive Artifact**

Artifact appears in Jenkins under **Build → Artifacts**.

---

## **Stage 8 — Email Notification**

Sends:

- Build summary  
- Installer attachment  
- Download URL  

---

# ------------------------------------------------------
# 3️⃣ STEP-BY-STEP JENKINS CREDENTIALS SETUP
# ------------------------------------------------------

## **3.1 SMTP Credentials (required for emailext)**

```
ID: smtp-user
ID: smtp-pass
```

---

## **3.2 GitHub Credentials (Required for private repos)**

```
ID: github-credentials
```

---

## **3.3 Credentials Usage in Jenkinsfile**

```
SMTP_USER = credentials('smtp-user')
SMTP_PASS = credentials('smtp-pass')
```

---

# -------------------------------------------------
# 4️⃣ PIPELINE EXECUTION PLAN — END TO END FLOW
# -------------------------------------------------

## **4.1 Start Build**

From Jenkins:

```
Build with Parameters
```

Select:

- VERSION_TYPE  
- ENVIRONMENT  

---

## **4.2 Behind the Scenes**

| Step | Description |
|------|-------------|
| 1 | Checkout repository |
| 2 | Read version |
| 3 | Bump version |
| 4 | Build application |
| 5 | Publish output |
| 6 | Generate build_config.iss |
| 7 | Run Inno Setup |
| 8 | Create installer |
| 9 | Archive artifact |
| 10 | Email notification |

---

## **4.3 Final Output**

```
result/v1.2.0_Minor.exe
```

---

# 🎉 **Documentation Complete**
Ready for GitHub, GitLab, Bitbucket, or enterprise CI/CD environments.

