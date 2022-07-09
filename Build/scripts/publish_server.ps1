# Ensure unsigned powershell script execution ist allowed: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

param (
    [Parameter(Mandatory=$false)] [String] $version,
    [Parameter(Mandatory=$false)] [String] $runtime,
    [Parameter(Mandatory=$false)] [String] $publshFolder
)

. $PSScriptRoot/shared.ps1

Push-Location $PSScriptRoot/../..

# Configure
$framework = "net6.0"
if (!$version){
    $version = git describe --tags
}

# Publish
Npm-Restore -folder Build
Publish-Project -project FS.TimeTracking.ReportServer/FS.TimeTracking.ReportServer.csproj -version $version -framework $framework -runtime $runtime -publshFolder $publshFolder

Pop-Location