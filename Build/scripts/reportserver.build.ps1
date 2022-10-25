# Ensure unsigned powershell script execution ist allowed: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

param (
)

. $PSScriptRoot/_core.ps1

Push-Location $PSScriptRoot/../..

# Configure
$version = git describe --tags

# Build
Npm-Restore -folder Build
Build-Project -project FS.TimeTracking.ReportServer -version $version

Pop-Location