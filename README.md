
# 🚀 Sample Flask Login (C# ASP.NET Core) — Complete CI/CD Documentation  
Windows Installer Build using **Inno Setup** + Automated **Jenkins Pipeline**

---

# 🧩 **1. Introduction**

This project is a **C# ASP.NET Core MVC Login Application** demonstrating:

- Secure user login with cookie authentication  
- Multi-environment support (Dev, QA, Prod)  
- Automated CI/CD using **Jenkins Declarative Pipeline**  
- Windows installer generation using **Inno Setup (ISCC.exe)**  
- Automatic version bumping (Alpha, Beta, Patch, Minor, Major)  
- Build artifact storage and email distribution  

This README provides **complete setup, installation, CI/CD design, GitHub setup, Jenkins configuration, and execution workflow**.

---

# 🛠️ **2. Software Used & Installation Steps**

This project requires the following software to be installed on the **Jenkins Windows Build Agent** and optionally on developer machines.

---

## ✅ **2.1 .NET SDK 6.0+**

Download:  
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

Verify installation:

```bash
dotnet --info
```

---

## ✅ **2.2 Inno Setup Compiler 6 (ISCC.exe)**  
Used to package the application into a `.exe` installer.

Download:  
https://jrsoftware.org/isdl.php

Install to default path:

```
C:\Program Files (x86)\Inno Setup 6```

Verify:

```powershell
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" /?
```

---

## ✅ **2.3 Jenkins (Latest LTS)**

Download:  
https://www.jenkins.io/download/

**Required Plugins:**
- Git Plugin  
- Pipeline Plugin  
- Email Extension Plugin  
- Credentials Plugin  
- Node Label Parameter Plugin  
- ANSI Color Plugin  

---

## ✅ **2.4 Git**

Download:  
https://git-scm.com/download/win

Verify:

```bash
git --version
```

---

## ✅ **2.5 Java 11 or Java 17 (Required by Jenkins)**

Download:  
https://adoptium.net/

Verify:

```bash
java -version
```

---

# 🏗️ **3. GitHub Project Setup — Step-by-Step**

This section explains how to create the GitHub repository for this project.

---

## ✔️ **Step 1: Create GitHub Repository**

1. Log in to GitHub → click **New Repository**
2. Repository name:  
   ```
   sample-flask-login-cicd
   ```
3. Choose:
   - Public or Private  
   - Add README.md → *Optional*  
4. Click **Create Repository**

---

## ✔️ **Step 2: Clone Repository Locally**

```bash
git clone https://github.com/<your-user>/sample-flask-login-cicd.git
```

---

## ✔️ **Step 3: Copy Project Files into Repo**

Place:

```
src/
installer/
config/
Jenkinsfile
README.md
version.txt
```

---

## ✔️ **Step 4: Commit & Push**

```bash
git add .
git commit -m "Initial project commit"
git push origin main
```

---

# 🔐 **4. Jenkins Credential Setup**

You must configure **SMTP**, **GitHub**, and optional secrets in Jenkins.

Navigate:

**Jenkins → Manage Jenkins → Credentials → System → Global Credentials → Add Credentials**

---

## 📧 **4.1 SMTP Credentials (For Email Sending)**

### Create Credential #1  
| Field | Value |
|------|--------|
| Kind | Username & Password |
| ID | smtp-user |
| Username | Your SMTP username |
| Password | Your SMTP password |

### Create Credential #2  
| Kind | Secret Text |
| ID | smtp-pass |
| Secret | Same SMTP password |

---

## 🧑‍💻 **4.2 GitHub Credentials**

| Field | Value |
|------|--------|
| Kind | Username + Password / Personal Access Token |
| ID | github-credentials |
| Username | GitHub username |
| Password | Personal Access Token |

This allows Jenkins to clone the private repo.

---

# 🧱 **5. Jenkins Pipeline Setup (With Full Explanation)**

Copy the provided `Jenkinsfile` into your repo.

Pipeline Parameters:

| Parameter | Purpose |
|----------|----------|
| VERSION_TYPE | Alpha, Beta, Patch, Minor, Major |
| ENVIRONMENT | Dev, QA, Prod |

---

## 📌 **Pipeline Stages — Deep Explanation**

### **Stage 1 — Checkout**
Pulls source code from GitHub.

---

### **Stage 2 — Read & Bump Version**

Reads version.txt:

```
1.0.0
```

Based on `VERSION_TYPE`, produces:

- `v1.0.0_Alpha`
- `v1.0.1_Patch`
- `v1.1.0_Minor`
- `v2.0.0_Major`

Installer filename becomes:

```
result/vX.Y.Z_Type.exe
```

---

### **Stage 3 — Build the C# Application**

Commands:

```bash
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish/
```

Produces:

```
publish/ (ready for packaging)
```

---

### **Stage 4 — Prepare Folders**

Ensures:

```
result/
publish/
```

exist.

---

### **Stage 5 — Generate Inno Setup Build Config**

Creates:

```
installer/build_config.iss
```

Which includes version metadata.

---

### **Stage 6 — Run Inno Setup Compiler**

Compiles:

```
publish/ → .exe Installer
```

Using:

```bash
ISCC.exe installer/build_config.iss
```

Output:

```
result/v1.1.0_Minor.exe
```

---

### **Stage 7 — Archive Artifact**

Installer becomes downloadable in Jenkins UI.

---

### **Stage 8 — Email Installer**

- Sends email  
- Attaches installer  
- Includes build details  

---

# ▶️ **6. Pipeline Execution Steps (Step-by-Step)**

Open Jenkins Job → **Build with Parameters**

### Select:

| Parameter | Example |
|----------|----------|
| VERSION_TYPE | Minor |
| ENVIRONMENT | QA |

Click **Build**.

---

## 🔄 **What Happens Internally?**

| Step | Description |
|------|-------------|
| 1 | Code is cloned |
| 2 | Version bumped |
| 3 | .NET project built |
| 4 | Publish output generated |
| 5 | Installer config generated |
| 6 | Installer EXE generated |
| 7 | Artifact archived |
| 8 | Email sent |

---

## 📦 **Final Output Format**

Installer generated:

```
result/v1.2.0_Minor.exe
```

Included in email + Jenkins artifacts.

---

# 📁 **7. Project Folder Structure (Complete)**

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
│       ├── Models/
│       ├── Services/
│       ├── Views/
│       └── wwwroot/
├── publish/ (auto-generated)
└── result/  (Installer output)
```

---

# 🎉 **README Completed**

This README.md contains:

✔ Introduction  
✔ Software + installation steps  
✔ GitHub repo creation guide  
✔ Jenkins credential setup  
✔ Detailed pipeline explanation  
✔ Execution workflow  
✔ Output format + release flow  
✔ Full project folder structure  

---

If you want a **PDF version**, **Confluence version**, or **HTML documentation**, I can generate that too.

