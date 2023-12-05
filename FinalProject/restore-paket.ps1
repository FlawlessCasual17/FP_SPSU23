# PLEASE RUN THIS FIRST OR ELSE THIS PROJECT WILL NOT BE ABLE TO WORK PROPERLY.
Set-Location $PSScriptRoot
$path = Resolve-Path ([System.IO.Path]::Combine($PSScriptRoot, '..', '.config', 'dotnet-tools.json')) -ErrorAction 'SilentlyContinue'
dotnet tool install paket --tool-manifest $path
dotnet tool restore --tool-manifest $path; dotnet restore
Set-Location '-'
