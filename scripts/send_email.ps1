param(
    [string]$To,
    [string]$Subject,
    [string]$Body,
    [string]$AttachmentPath
)

Write-Host "=========================================="
Write-Host " 📧 Email Helper Script (PowerShell)"
Write-Host "=========================================="

Write-Host "[send_email] Note: Jenkins 'emailext' plugin handles actual sending."
Write-Host "[send_email] This script exists only for optional manual/local usage."

Write-Host "------------------------------------------"
Write-Host " To:          $To"
Write-Host " Subject:     $Subject"
Write-Host " Attachment:  $AttachmentPath"
Write-Host "------------------------------------------"
Write-Host " Body:"
Write-Host " $Body"
Write-Host "------------------------------------------"

# -----------------------------------------------------------------------------
# Optional: Implement native SMTP logic if needed
# Example:
# Send-MailMessage -To $To -From "noreply@example.com" -Subject $Subject `
#     -Body $Body -SmtpServer "smtp.example.com" -Attachments $AttachmentPath
# -----------------------------------------------------------------------------

Write-Host "[send_email] Execution complete."
