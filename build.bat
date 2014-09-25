@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)
 
set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)
 
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild WebApiProxy.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
 
mkdir Build
mkdir Build\lib
mkdir Build\lib\net40
 
echo Creating WebProxyApi Package
%nuget% pack "WebApiProxy.Server\WebApiProxy.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
if errorlevel 1 echo Error creating WebApiProxy Package

echo Creating WebProxyApi CSharp Package
%nuget% pack "WebApiProxy.Tasks\WebApiProxy.CSharp.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
if errorlevel 1 echo Error creating WebApiProxy CSharp Package