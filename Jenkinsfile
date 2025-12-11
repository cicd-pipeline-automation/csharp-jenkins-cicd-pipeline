// ============================================================================
// 🚀 Production-Ready Jenkins Declarative Pipeline
// 📦 Project: Sample Flask Login (C# - ASP.NET Core)
// ⚙️ Build Tool: Inno Setup Compiler (ISCC)
// 📧 Email Notification with Build Artifact (*.exe)
// ============================================================================

pipeline {
    agent any

    // ------------------------------------------------------------------------
    // 🔧 Pipeline Parameters
    // ------------------------------------------------------------------------
    parameters {
        choice(
            name: 'VERSION_TYPE',
            choices: ['Alpha', 'Beta', 'Patch', 'Minor', 'Major'],
            description: 'Select Version Type'
        )

        choice(
            name: 'ENVIRONMENT',
            choices: ['Dev', 'QA', 'Prod'],
            description: 'Select Deployment Environment'
        )
    }

    // ------------------------------------------------------------------------
    // 🌍 Global Environment Variables
    // ------------------------------------------------------------------------
    environment {
        PROJECT_NAME = "Sample Flask Login"
    }

    stages {

        // --------------------------------------------------------------------
        stage('Checkout Source Code') {
            steps {
                echo "📥 Checking out repository..."
                checkout scm
            }
        }

        // --------------------------------------------------------------------
        stage('Read & Bump Version') {
            steps {
                script {
                    echo "🔢 Reading version information..."

                    // Create version file if not present
                    if (!fileExists('version.txt')) {
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
                    env.NEW_VERSION = "v${numeric}_${params.VERSION_TYPE}"

                    // Update version file
                    writeFile file: 'version.txt', text: numeric

                    echo "Updated Version (numeric): ${numeric}"
                    echo "Full Version: ${env.NEW_VERSION}"
                }
            }
        }

        // --------------------------------------------------------------------
        stage('Restore, Build & Publish') {
            steps {
                echo "⚙️ Restoring, building, and publishing the application..."
                sh """
                    dotnet restore src/SampleFlaskLogin.sln
                    dotnet build src/SampleFlaskLogin.sln -c Release
                    dotnet publish src/SampleFlaskLogin/ -c Release -o publish/
                """
            }
        }

        // --------------------------------------------------------------------
        stage('Prepare Output Folders') {
            steps {
                echo "📁 Preparing result folder..."
                sh 'mkdir -p result'
            }
        }

        // --------------------------------------------------------------------
        stage('Generate Inno Setup Build Config') {
            steps {
                script {
                    echo "📝 Generating dynamic Inno Setup build configuration..."

                    def buildConfig = """
#define MyAppVersion "${env.NEW_VERSION}"
#define MyOutputFile "result\\\\${env.NEW_VERSION}.exe"
#include "installer_script.iss"
"""

                    writeFile file: 'installer/build_config.iss', text: buildConfig
                }
            }
        }

        // --------------------------------------------------------------------
        stage('Compile Installer (Inno Setup)') {
            steps {
                echo "🛠️ Running Inno Setup Compiler..."
                bat "\"C:\\\\Program Files (x86)\\\\Inno Setup 6\\\\ISCC.exe\" installer\\\\build_config.iss"
            }
        }

        // --------------------------------------------------------------------
        stage('Archive Build Artifact') {
            steps {
                echo "📦 Archiving generated EXE file..."
                archiveArtifacts artifacts: 'result/*.exe', fingerprint: true
            }
        }

        // --------------------------------------------------------------------
        stage('Send Email Notification') {
            steps {
                echo "📧 Sending email notification with build artifacts..."
                emailext(
                    subject: "Build Complete: ${env.PROJECT_NAME} - ${env.NEW_VERSION}",
                    body: """
Hello Team,

The build has completed successfully.

**Project:** ${env.PROJECT_NAME}  
**Environment:** ${params.ENVIRONMENT}  
**Version:** ${env.NEW_VERSION}  

Download the installer:  
${BUILD_URL}artifact/result/

Regards,  
Jenkins CI/CD
""",
                    to: 'devopsuser8413@gmail.com',
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
            emailext(
                subject: "❌ BUILD FAILED - ${env.PROJECT_NAME}",
                body: """
The build has FAILED.

Please review the build logs:  
${BUILD_URL}

Regards,  
Jenkins CI/CD
""",
                to: 'devopsuser8413@gmail.com'
            )
        }
    }
}
