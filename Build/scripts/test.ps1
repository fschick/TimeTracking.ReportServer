# Ensure unsigned powershell script execution is allowed: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned

param (
    [Parameter(Mandatory=$false)] [String] $pipelineId,
    [Parameter(Mandatory=$false)] [String] $process,
    [Parameter(Mandatory=$false)] [String] $processVersion
)

. $PSScriptRoot/shared.ps1

Push-Location $PSScriptRoot/../..

# Configure
$version = git describe --tags

# Run tests
# Test-Project -project FS.TimeTracking.ReportServer.Tests/FS.TimeTracking.ReportServer.Tests.sln -version $version

Pop-Location