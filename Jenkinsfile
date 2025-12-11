// ============================================================================
// 🚀 Production-Ready Jenkins Declarative Pipeline
// 📦 Project: Sample Flask Login (C# - ASP.NET Core)
// ⚙️ Build Tool: Inno Setup Compiler (ISCC)
// 📧 Email Notification with Build Artifact (*.exe)
// ============================================================================

pipeline {
    agent any

    // ------------------------------------------------------------------------
    // ⚙️ Pipeline Options
    // ------------------------------------------------------------------------
    options {
        disableConcurrentBuilds()
        timestamps()
    }

    // ------------------------------------------------------------------------
    // 🔧 Pipeline Parameters
    // ------------------------------------------------------------------------
    parameters {
        choice(
            name: 'VERSION_TYPE',
            choices: ['Alpha', 'Beta', 'Patch', 'Minor', 'Major'],
            description: 'Select Version Type for release naming and version bumping'
        )

        choice(
            name: 'ENVIRONMENT',
            choices: ['Dev', 'QA', 'Prod'],
            description: 'Select target environment (for reporting/metadata)'
        )
    }

    // ------------------------------------------------------------------------
    // 🌍 Global Environment Variables
    // ------------------------------------------------------------------------
    environment {
        PROJECT_NAME      = "Sample Flask Login"
        NOTIFY_EMAIL      = "devopsuser8413@gmail.com"   // Change if needed
        DOTNET_PATH       = "C:\\Program Files\\dotnet\\dotnet.exe"
        INNO_SETUP_PATH   = "C:\\Program Files (x86)\\Inno Setup 6\\ISCC.exe"
    }

    stages {
        // --------------------------------------------------------------------
        // 🔢 Version Handling
        // --------------------------------------------------------------------
        stage('Read & Bump Version') {
            steps {
                script {
                    echo "🔢 Reading version information..."

                    // Create version file if not present
                    if (!fileExists('version.txt')) {
                        echo "version.txt not found. Initializing with 1.0.0"
                        writeFile file: 'version.txt', text: '1.0.0'
                    }

                    // Parse version components
                    def ver = readFile('version.txt').trim()
                    def parts = ver.tokenize('.')

                    def major = parts[0].toInteger()
                    def minor = parts[1].toInteger()
                    def patch = parts[2].toInteger()

                    echo "Current Version: ${major}.${minor}.${patch}"

                    // Apply version bumping logic
                    switch (params.VERSION_TYPE) {
                        case "Major":
                            major += 1; minor = 0; patch = 0; break
                        case "Minor":
                            minor += 1; patch = 0; break
                        case "Patch":
                            patch += 1; break
                        case "Alpha":
                        case "Beta":
                            // Numeric version unchanged
                            break
                    }

                    // Construct new version strings
                    def numeric = "${major}.${minor}.${patch}"
                    env.NEW_VERSION_NUMERIC = numeric
                    env.NEW_VERSION         = "v${numeric}_${params.VERSION_TYPE}"

                    // Update version file
                    writeFile file: 'version.txt', text: numeric

                    echo "✅ Updated Version (numeric): ${numeric}"
                    echo "🏷️ Full Version Label   : ${env.NEW_VERSION}"
                }
            }
        }

        // --------------------------------------------------------------------
        // 🧱 Restore, Build & Publish
        // --------------------------------------------------------------------
        stage('Restore, Build & Publish') {
            steps {
                echo "⚙️ Restoring, building, and publishing the application..."

                // Verify dotnet
                bat "\"%DOTNET_PATH%\" --version"

                // Restore
                bat "\"%DOTNET_PATH%\" restore src\\SampleFlaskLogin.sln"

                // Build
                bat "\"%DOTNET_PATH%\" build src\\SampleFlaskLogin.sln -c Release"

                // Publish
                bat "\"%DOTNET_PATH%\" publish src\\SampleFlaskLogin\\ -c Release -o publish"
            }
        }

        // --------------------------------------------------------------------
        // 📁 Prepare Output Folders
        // --------------------------------------------------------------------
        stage('Prepare Output Folders') {
            steps {
                echo "📁 Preparing result folder..."
                bat '''
                    if not exist result mkdir result
                '''
            }
        }

        // --------------------------------------------------------------------
        // 📝 Generate Inno Setup Build Config
        // --------------------------------------------------------------------
        stage('Generate Inno Setup Build Config') {
            steps {
                echo "📝 Generating Inno Setup build configuration..."

                writeFile file: 'installer/build_config.iss', text: """
                [Setup]
                AppName=SampleFlaskLogin
                AppVersion=${FULL_VERSION}
                DefaultDirName={pf}\\SampleFlaskLogin
                DefaultGroupName=SampleFlaskLogin
                OutputDir=..\\result
                OutputBaseFilename=SampleFlaskLoginInstaller

                [Files]
                Source: "publish/*"; DestDir: "{app}"; Flags: recursesubdirs

                [Icons]
                Name: "{group}\\SampleFlaskLogin"; Filename: "{app}\\SampleFlaskLogin.exe"
                """
            }
        }

        // --------------------------------------------------------------------
        // 🛠️ Compile Installer (Inno Setup)
        // --------------------------------------------------------------------
        stage('Compile Installer (Inno Setup)') {
            steps {
                echo "🛠️ Running Inno Setup Compiler..."

                bat "\"${INNO_SETUP_PATH}\" installer\\build_config.iss"
            }
        }

        // --------------------------------------------------------------------
        // 📦 Archive Build Artifact
        // --------------------------------------------------------------------
        stage('Archive Build Artifact') {
            steps {
                echo "📦 Archiving generated EXE file..."
                archiveArtifacts artifacts: 'result/*.exe', fingerprint: true
            }
        }

        // --------------------------------------------------------------------
        // 📧 Send Email Notification
        // --------------------------------------------------------------------
        stage('Send Email Notification') {
            steps {
                echo "📧 Sending email notification with build artifacts..."
                emailext(
                    subject: "✅ Build Complete: ${env.PROJECT_NAME} - ${env.NEW_VERSION}",
                    body: """
                            Hello Team,

                            The build has completed successfully. 🎉

                            Project     : ${env.PROJECT_NAME}
                            Environment : ${params.ENVIRONMENT}
                            Version     : ${env.NEW_VERSION}

                            You can download the installer from Jenkins:

                            ${BUILD_URL}artifact/result/

                            Regards,
                            Jenkins CI/CD
                            """,
                    to: "${env.NOTIFY_EMAIL}",
                    attachmentsPattern: 'result/*.exe'
                )
            }
        }
    }

    // ------------------------------------------------------------------------
    // 🚨 Post Actions (Failure Handling)
    // ------------------------------------------------------------------------
    post {
        failure {
            echo "❌ Build failed. Sending failure notification email..."
            emailext(
                subject: "❌ BUILD FAILED - ${env.PROJECT_NAME}",
                body: """
                        The build has *FAILED*. ❗

                        Please review the build logs here:

                        ${BUILD_URL}

                        Project     : ${env.PROJECT_NAME}
                        Environment : ${params.ENVIRONMENT}

                        Regards,
                        Jenkins CI/CD
                        """,
                to: "${env.NOTIFY_EMAIL}"
            )
        }
    }
}
