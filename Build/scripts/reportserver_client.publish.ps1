# Ensure unsigned powershell script execution ist allowed: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

param (
    [Parameter(Mandatory=$false)] [String] $version,
    [Parameter(Mandatory=$false)] [String] $publshFolder,
	[Parameter(Mandatory=$false)] [String] $nugetUrl, 
	[Parameter(Mandatory=$false)] [String] $apiKey
)

. $PSScriptRoot/_core.ps1

Push-Location $PSScriptRoot/../..

# Configure
$framework = "net8.0"
if (!$version){
    $version = git describe --tags
}

# Publish
Npm-Restore -folder Build
Build-Project -project FS.TimeTracking.ReportServer -version $version
Publish-Nuget -project FS.TimeTracking.ReportServer.Client/src/FS.TimeTracking.Report.Client -version $version -publshFolder $publshFolder
Push-Nuget -project -publshFolder $publshFolder -serverUrl $nugetUrl -apiKey $apiKey

Pop-Location