@echo off
set pathToBatch=%~dp0
pushd %pathToBatch%

set serviceName=FS.TimeTracking.ReportServer
set executable=FS.TimeTracking.ReportServer.exe
sc create %serviceName% binPath= "%pathToBatch%%executable%" start= delayed-auto
sc start %serviceName%

popd %pathToBatch%
pause