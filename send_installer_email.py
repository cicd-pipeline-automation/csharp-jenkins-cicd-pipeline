import os
import smtplib
from email.message import EmailMessage

# -------------------------------------------------
# READ VALUES FROM JENKINS ENVIRONMENT
# -------------------------------------------------
SMTP_SERVER   = os.getenv("SMTP_SERVER", "smtp.gmail.com")
SMTP_PORT     = int(os.getenv("SMTP_PORT", "587"))

SMTP_USERNAME = os.getenv("SMTP_USERNAME")   # Gmail address
SMTP_PASSWORD = os.getenv("SMTP_PASSWORD")   # Gmail App Password

FROM_EMAIL = os.getenv("FROM_EMAIL")
TO_EMAILS  = os.getenv("TO_EMAILS", "").split(",")

SUBJECT = os.getenv("EMAIL_SUBJECT", "Jenkins Build – Installer Attached")
BODY    = os.getenv("EMAIL_BODY", "Please find the installer attached.")

# -------------------------------------------------
# FILE PATH
# -------------------------------------------------
WORKSPACE = os.getenv("WORKSPACE", os.getcwd())
ZIP_PATH  = os.path.join(WORKSPACE, "result", "installer.zip")

# -------------------------------------------------
# VALIDATION
# -------------------------------------------------
if not os.path.exists(ZIP_PATH):
    raise FileNotFoundError(f"installer.zip not found at: {ZIP_PATH}")

if not SMTP_USERNAME or not SMTP_PASSWORD:
    raise ValueError("SMTP_USERNAME and SMTP_PASSWORD must be set")

if not FROM_EMAIL or not TO_EMAILS or TO_EMAILS == [""]:
    raise ValueError("FROM_EMAIL or TO_EMAILS not set")

# -------------------------------------------------
# EMAIL CREATION
# -------------------------------------------------
msg = EmailMessage()
msg["From"] = FROM_EMAIL
msg["To"] = ", ".join(TO_EMAILS)
msg["Subject"] = SUBJECT
msg.set_content(BODY)

with open(ZIP_PATH, "rb") as f:
    msg.add_attachment(
        f.read(),
        maintype="application",
        subtype="zip",
        filename="installer.zip"
    )

# -------------------------------------------------
# SEND EMAIL (GMAIL SMTP 587 + STARTTLS)
# -------------------------------------------------
with smtplib.SMTP(SMTP_SERVER, SMTP_PORT) as server:
    server.ehlo()
    server.starttls()              # 🔐 REQUIRED for port 587
    server.ehlo()
    server.login(SMTP_USERNAME, SMTP_PASSWORD)
    server.send_message(msg)

print("✅ Email sent successfully via Gmail SMTP (587) with installer.zip attached")
