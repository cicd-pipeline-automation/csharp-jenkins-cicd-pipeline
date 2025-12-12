import os
import smtplib
from email.message import EmailMessage

SMTP_SERVER   = os.getenv("SMTP_SERVER")
SMTP_PORT     = int(os.getenv("SMTP_PORT", "587"))
SMTP_USERNAME = os.getenv("SMTP_USERNAME")
SMTP_PASSWORD = os.getenv("SMTP_PASSWORD")

FROM_EMAIL = os.getenv("FROM_EMAIL")
TO_EMAILS  = os.getenv("TO_EMAILS", "").split(",")

PROJECT_NAME = os.getenv("PROJECT_NAME")
BUILD_URL    = os.getenv("BUILD_URL")

SUBJECT = f"{PROJECT_NAME} Installer Build – SUCCESS"
BODY = f"""
Hello Team,

The build completed successfully 🎉

Project : {PROJECT_NAME}

Download installer from Jenkins:
{BUILD_URL}artifact/result/installer.zip

Regards,
Jenkins CI/CD
"""

msg = EmailMessage()
msg["From"] = FROM_EMAIL
msg["To"] = ", ".join(TO_EMAILS)
msg["Subject"] = SUBJECT
msg.set_content(BODY)

with smtplib.SMTP(SMTP_SERVER, SMTP_PORT) as server:
    server.ehlo()
    server.starttls()
    server.login(SMTP_USERNAME, SMTP_PASSWORD)
    server.send_message(msg)

print("✅ Email sent successfully (link-only, Gmail safe)")
