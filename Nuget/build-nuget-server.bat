@echo off
set path=.
if not [%1]==[] set path=%1

IF NOT EXIST Packages MKDIR Packages

echo Creating WebProxyApi Package
 %path%\..\.nuget\nuget pack %path%\..\WebApiProxy.Server\WebApiProxy.Server.csproj  -Build -IncludeReferencedProjects -OutputDirectory %path%\..\Nuget\Packages -Properties Configuration=Release -Verbose 

if errorlevel 1 echo Error creating WebApiProxy Package

pause;