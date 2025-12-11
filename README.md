# README.md — Sample Flask Login (C# ASP.NET Core) CI/CD Pipeline with Inno Setup & Jenkins

## 📌 Overview

This project is a **C# ASP.NET Core MVC Login Application**, packaged into a **Windows Installer (.exe)** using **Inno Setup**, and fully automated via a **Jenkins CI/CD Pipeline**.

The pipeline:

- Builds the .NET application  
- Publishes artifacts  
- Generates installer configuration  
- Compiles the Windows installer  
- Automatically bumps the version (Alpha, Beta, Patch, Minor, Major)  
- Stores the installer in the `result/` folder  
- Emails the `.exe` installer to configured recipients  

---

# 1. Software Installation Steps

## 1.1 Install .NET SDK 6.0+
Download from: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

Verify:
```
dotnet --info
```

## 1.2 Install Inno Setup Compiler
Download: https://jrsoftware.org/isdl.php

Install to:
```
C:\Program Files (x86)\Inno Setup 6\
```

## 1.3 Install Jenkins
Download: https://www.jenkins.io/download/

Ensure plugins:
- Email Extension
- Git
- Pipeline
- Credentials

## 1.4 Install Git
Download: https://git-scm.com/download/win

## 1.5 Install Java (Adoptium 11+)
https://adoptium.net/

## 1.6 Windows Build Agent Requirements
| Dependency | Required | Notes |
|-----------|----------|-------|
| .NET 6 SDK | Yes | Build & publish |
| Inno Setup 6 | Yes | Compile installer |
| Git | Optional | Source checkout |
| SMTP connectivity | Optional | Email |

---

# 2. Pipeline Detailed Explanation (Stage-by-Stage)

## Stage 1 — Checkout
Pulls source from Git.

## Stage 2 — Read & Bump Version
Reads version.txt and bumps version depending on:
- Alpha / Beta → no numeric change
- Patch → +1 patch
- Minor → +1 minor, reset patch
- Major → +1 major, reset minor+patch

Final version format:
```
v<MAJOR>.<MINOR>.<PATCH>_<TYPE>
```

## Stage 3 — Restore & Build (.NET)
```
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish/
```

## Stage 4 — Prepare Folders
Ensures `result/` exists.

## Stage 5 — Generate Build Config
Creates `installer/build_config.iss`.

## Stage 6 — Run Inno Setup Compiler
Produces installer:
```
result/v1.0.0_Alpha.exe
```

## Stage 7 — Archive Artifact
Jenkins saves executable.

## Stage 8 — Email Notification
Sends `.exe` attachment to team.

## Post-Failure Action
Emails failure notification.

---

# 3. Step-by-Step Jenkins Credential Setup

## 3.1 Open Jenkins Credential Manager
Manage Jenkins → Credentials → System → Global Credentials → Add Credentials

## 3.2 SMTP Credentials Setup
Create:
- ID: smtp-user  
- ID: smtp-pass  

## 3.3 GitHub Credentials
If private repo:
- ID: github-credentials  

## 3.4 Verify in Jenkinsfile
```
SMTP_USER = credentials('smtp-user')
SMTP_PASS = credentials('smtp-pass')
```

---

# 4. Pipeline Execution Plan

## 4.1 Trigger Pipeline
Jenkins → Build with Parameters

Select:
- VERSION_TYPE
- ENVIRONMENT

Then click **Build**.

## 4.2 Full Build Flow

| Step | Action |
|------|--------|
| 1 | Checkout code |
| 2 | Bump version |
| 3 | Build .NET code |
| 4 | Publish output |
| 5 | Generate Inno Setup config |
| 6 | Build installer |
| 7 | Archive result |
| 8 | Send email |
| 9 | Mark build status |

## 4.3 After Build
Installer available in Jenkins Artifacts.

## 4.4 Deployment (Optional)
Installer can be uploaded to QA/Prod or automated via WinRM/Ansible.

---
