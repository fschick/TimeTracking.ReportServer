# TimeTracking Report Server

Report server for [TimeTracking](https://github.com/fschick/TimeTracking)

## Supported operating systems

Supported operation systems are Windows 10 + / Windows Server 2016 + / Debian based Linux

Other Operating systems might worked but are untested

## Installation

### Run as docker container

```bash
docker run -d -p <port_on_host>:5000 \
	--name timetracking.report \
	schicksoftware/timetracking.reportserver
```

### Run from command line

```shell
# Windows
.\FS.TimeTracking.ReportServer.exe

# Linux
chmod +x ./FS.TimeTracking.ReportServer
./FS.TimeTracking.ReportServer
```

### Install as Windows service

```bash
# Copy the content of publish folder to suitable location
mkdir C:\services\TimeTracking.ReportServer
robocopy /E timetracking.ReportServer.1.0.1.windows\ C:\Services\TimeTracking.ReportServer

# When the service runs from program folder adjust path to log files (programm folder isn't writeable!)
cd C:\services\TimeTracking.ReportServer
notepad config\FS.TimeTracking.ReportServer.config.nlog

# Install and run as windows service
# The service will be installed as "FS.TimeTracking". You can change the name in the .bat file
cd C:\services\TimeTracking.ReportServer
FS.TimeTracking.ReportServer.service.install.bat

# Uninstall service
cd C:\services\TimeTracking.ReportServer
FS.TimeTracking.ReportServer.service.uninstall.bat
```

### Install as Linux daemon

```bash
# Copy files
cp -r timetracking.1.0.1.linux/opt/FS.TimeTracking.ReportServer/ /opt/
cp timetracking.1.0.1.linux/etc/systemd/system/FS.TimeTracking.ReportServer.service.template /etc/systemd/system/FS.TimeTracking.ReportServer.service
chmod +x /opt/FS.TimeTracking/bin/FS.TimeTracking.ReportServer

# Create system user/group
useradd -m dotnetuser --user-group
# Adjust user/group
vi /etc/systemd/system/FS.TimeTracking.ReportServer.service
chown -R dotnetuser:dotnetuser /opt/FS.TimeTracking.ReportServer/
chmod -R o= /opt/FS.TimeTracking.ReportServer/

# Test service
/opt/FS.TimeTracking/bin/FS.TimeTracking.ReportServer

# Start service
systemctl daemon-reload
systemctl start FS.TimeTracking.ReportServer.service

# Get service status
systemctl status FS.TimeTracking.ReportServer.service
journalctl -u FS.TimeTracking.ReportServer.service

# If you want your service to start when the machine does then you can use
systemctl enable FS.TimeTracking.ReportServer.service
```

## Configuration

##### Application / Service / Kestrel configuration

./config/FS.TimeTracking.ReportServer.config.json

```json
"TimeTrackingReport": {
  "StimulsoftLicenseKey": "<Stimulsoft License>"
}
```

##### Logging configuration

./config/FS.TimeTracking.ReportServer.config.nlog

See [official documentation](https://github.com/nlog/nlog/wiki/Configuration-file) for details

## Development

### Pre requirements

[.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)

### Run

Download or clone repository

```bash
git clone https://github.com/fschick/TimeTracking.ReportServer.git
cd TimeTracking.ReportServer
```

Run server

```
dotnet run --project FS.TimeTracking.ReportServer/FS.TimeTracking.csproj
```

### Publish

See publish script `Build/scripts/reportserver.publish.ps1`
