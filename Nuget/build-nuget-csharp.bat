@echo off
set path=.
if not [%1]==[] set path=%1

echo Creating WebProxyApi CSharp Package
 %path%\..\.nuget\nuget pack %path%\..\WebApiProxy.Tasks\WebApiProxy.Tasks.csproj -IncludeReferencedProjects -OutputDirectory %path%\Packages

if errorlevel 1 echo Error creating WebApiProxy CSharp Package

pause;