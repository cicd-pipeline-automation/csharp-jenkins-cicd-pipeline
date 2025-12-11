; ============================================================================
; 🛠 Dynamic Build Configuration for Inno Setup
; ----------------------------------------------------------------------------
; This file is generated automatically by Jenkins during the CI/CD pipeline.
; The Jenkins pipeline injects the real values for:
;     - MyAppVersion  (e.g., v1.0.3_Patch)
;     - MyOutputFile  (e.g., result\v1.0.3_Patch.exe)
; After defining variables, this file includes the main installer script:
;     installer_script.iss
; ============================================================================

#define MyAppVersion "v1.0.0_Alpha"
#define MyOutputFile "result\\v1.0.0_Alpha.exe"

#include "installer_script.iss"
