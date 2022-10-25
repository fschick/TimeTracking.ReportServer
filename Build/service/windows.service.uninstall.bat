@echo off
set pathToBatch=%~dp0
pushd %pathToBatch%

set serviceName=FS.TimeTracking.ReportServer
sc stop %serviceName%
sc delete %serviceName%

popd %pathToBatch%
pause