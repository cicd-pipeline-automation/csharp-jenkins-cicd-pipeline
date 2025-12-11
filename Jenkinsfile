// ============================================================================
// 🚀 Production-Ready Jenkins Declarative Pipeline
// 📦 Project: Sample Flask Login (C# - ASP.NET Core)
// ⚙️ Build Tool: Inno Setup Compiler (ISCC)
// 📧 Email Notification with Build Artifact (*.zip)
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
        NOTIFY_EMAIL      = "devopsuser8413@gmail.com"
        DOTNET_PATH       = "C:\\Program Files\\dotnet\\dotnet.exe"
        INNO_SETUP_PATH   = "C:\\Program Files (x86)\\Inno Setup 6\\ISCC.exe"
    }

    stages {

        // --------------------------------------------------------------------
        // 🔢 Read & Bump Version
        // --------------------------------------------------------------------
        stage('Read & Bump Version') {
            steps {
                script {
                    echo "🔢 Reading version information..."

                    if (!fileExists('version.txt')) {
                        writeFile file: 'version.txt', text: '1.0.0'
                    }

                    def ver = readFile('version.txt').trim()
                    def parts = ver.tokenize('.')

                    def major = parts[0].toInteger()
                    def minor = parts[1].toInteger()
                    def patch = parts[2].toInteger()

                    switch (params.VERSION_TYPE) {
                        case "Major": major += 1; minor = 0; patch = 0; break
                        case "Minor": minor += 1; patch = 0; break
                        case "Patch": patch += 1; break
                        case "Alpha":
                        case "Beta": break // numeric version same
                    }

                    def numeric = "${major}.${minor}.${patch}"
                    env.NEW_VERSION = "v${numeric}_${params.VERSION_TYPE}"

                    writeFile file: 'version.txt', text: numeric

                    echo "🏷️ Full Version Label: ${env.NEW_VERSION}"
                }
            }
        }

        // --------------------------------------------------------------------
        // 🔧 Restore, Build & Publish
        // --------------------------------------------------------------------
        stage('Restore, Build & Publish') {
            steps {
                echo "⚙️ Building C# project..."

                bat "\"%DOTNET_PATH%\" restore src\\SampleFlaskLogin.sln"
                bat "\"%DOTNET_PATH%\" build src\\SampleFlaskLogin.sln -c Release"
                bat "\"%DOTNET_PATH%\" publish src\\SampleFlaskLogin\\ -c Release -o publish"
            }
        }

        // --------------------------------------------------------------------
        // 📁 Prepare Output Folder
        // --------------------------------------------------------------------
        stage('Prepare Output Folder') {
            steps {
                bat 'if not exist result mkdir result'
            }
        }

        // --------------------------------------------------------------------
        // 📝 Generate Inno Setup Build Config
        // --------------------------------------------------------------------
        stage('Generate Inno Setup Build Config') {
            steps {
                writeFile file: 'installer/build_config.iss', text: """
                    [Setup]
                    AppName=SampleFlaskLogin
                    AppVersion=${env.NEW_VERSION}
                    DefaultDirName={pf}\\SampleFlaskLogin
                    DefaultGroupName=SampleFlaskLogin
                    OutputDir=..\\result
                    OutputBaseFilename=SampleFlaskLoginInstaller_${env.NEW_VERSION}

                    [Files]
                    Source: "..\\publish\\*"; DestDir: "{app}"; Flags: recursesubdirs

                    [Icons]
                    Name: "{group}\\SampleFlaskLogin"; Filename: "{app}\\SampleFlaskLogin.exe"
                    """
            }
        }

        // --------------------------------------------------------------------
        // 🛠️ Compile Installer
        // --------------------------------------------------------------------
        stage('Compile Installer (Inno Setup)') {
            steps {
                bat "\"${INNO_SETUP_PATH}\" installer\\build_config.iss"
            }
        }

        // --------------------------------------------------------------------
        // 📦 Compress Installer as ZIP (Gmail Compatible)
        // --------------------------------------------------------------------
        stage('Compress Installer') {
            steps {
                echo "📦 Creating ZIP archive..."

                bat '''
                    powershell -Command "Compress-Archive -Path result\\*.exe -DestinationPath result\\installer.zip -Force"
                '''
            }
        }

        // --------------------------------------------------------------------
        // 🗂 Archive Artifacts
        // --------------------------------------------------------------------
        stage('Archive Build Artifact') {
            steps {
                archiveArtifacts artifacts: 'result/*.zip', fingerprint: true
            }
        }

        // --------------------------------------------------------------------
        // 📧 Send Email Notification
        // --------------------------------------------------------------------
        stage('Send Email Notification') {
            steps {
                emailext(
                    subject: "✅ Build Complete: ${env.PROJECT_NAME} - ${env.NEW_VERSION}",
                    body: """
                        Hello Team,

                        The build completed successfully 🎉

                        Project     : ${env.PROJECT_NAME}
                        Environment : ${params.ENVIRONMENT}
                        Version     : ${env.NEW_VERSION}

                        Download installer ZIP:
                        ${BUILD_URL}artifact/result/installer.zip

                        Regards,
                        Jenkins CI/CD
                        """,
                    attachmentsPattern: 'result/*.zip',
                    to: "${env.NOTIFY_EMAIL}"
                )
            }
        }
    }

    // ------------------------------------------------------------------------
    // ❌ Failure Notification
    // ------------------------------------------------------------------------
    post {
        failure {
            emailext(
                subject: "❌ BUILD FAILED - ${env.PROJECT_NAME}",
                body: """
                    Build FAILED ❗

                    Please check logs:
                    ${BUILD_URL}

                    Project     : ${env.PROJECT_NAME}
                    Environment : ${params.ENVIRONMENT}
                    """,
                to: "${env.NOTIFY_EMAIL}"
            )
        }
    }
}
