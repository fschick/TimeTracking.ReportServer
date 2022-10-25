# Ensure unsigned powershell script execution ist allowed: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

param (
    [Parameter(Mandatory=$false)] [String] $version,
    [Parameter(Mandatory=$false)] [String] $runtime,
    [Parameter(Mandatory=$false)] [String] $publshFolder
)

. $PSScriptRoot/_core.ps1

Push-Location $PSScriptRoot/../..

# Configure
$framework = "net6.0"
if (!$version){
    $version = git describe --tags
}

# Publish
Npm-Restore -folder Build
Publish-Project -project FS.TimeTracking.ReportServer/FS.TimeTracking.ReportServer.csproj -version $version -framework $framework -runtime $runtime -publshFolder $publshFolder
if ($runtime.StartsWith("win")) {
	Copy-Item ./Build/service/windows.service.install.bat $publshFolder/FS.TimeTracking.ReportServer.service.install.bat
	Copy-Item ./Build/service/windows.service.uninstall.bat $publshFolder/FS.TimeTracking.ReportServer.service.uninstall.bat
} else {
	Copy-Item ./Build/service/linux.service.template $publshFolder/FS.TimeTracking.ReportServer.service.template
}

Pop-Location