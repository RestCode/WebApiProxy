param($installPath, $toolsPath, $package)

Add-Type -Path (Join-Path $installPath "lib\net45\build\WebApiProxy.Tasks.dll")
Import-Module (Join-Path $toolsPath "WebApiProxyCSharp.psm1") -DisableNameChecking