# install-dotnet8-sdk.ps1  — installs the .NET 8 SDK (build tools)
$ErrorActionPreference = "Stop"
$outFile = "$env:TEMP\dotnet8-sdk.exe"

Remove-Item $outFile -ErrorAction SilentlyContinue

Write-Host "Downloading .NET 8 SDK..." -ForegroundColor Cyan
Import-Module BitsTransfer
Start-BitsTransfer -Source "https://aka.ms/dotnet/8.0/dotnet-sdk-win-x64.exe" -Destination $outFile

$size = (Get-Item $outFile).Length / 1MB
Write-Host ("Downloaded {0:N1} MB" -f $size) -ForegroundColor Cyan
if ($size -lt 100) { throw "Download too small - failed." }

Write-Host "Installing SDK silently..." -ForegroundColor Cyan
Start-Process -FilePath $outFile -ArgumentList "/install","/quiet","/norestart" -Wait -PassThru

Remove-Item $outFile -ErrorAction SilentlyContinue
Write-Host "Done. Close and reopen your shell, then run: dotnet --version" -ForegroundColor Green