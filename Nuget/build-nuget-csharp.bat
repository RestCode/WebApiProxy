@echo off
set path=.
if not [%1]==[] set path=%1

IF NOT EXIST Packages MKDIR Packages

echo Creating WebProxyApi CSharp Package
 %path%\..\.nuget\nuget pack %path%\..\WebApiProxy.Tasks\WebApiProxy.Tasks.csproj -IncludeReferencedProjects -OutputDirectory %path%\Packages -Properties Configuration=Release -Verbose 

if errorlevel 1 echo Error creating WebApiProxy CSharp Package

pause;