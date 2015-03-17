param($installPath, $toolsPath, $package)

Add-Type -Path (Join-Path $installPath "build\WebApiProxy.Tasks.dll")
Import-Module (Join-Path $toolsPath "WebApiProxyCSharp.psm1") -DisableNameChecking