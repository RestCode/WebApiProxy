param($installPath, $toolsPath, $package, $project)
   copy-item $toolsPath\*.snippet -destination ([System.Environment]::ExpandEnvironmentVariables("%VisualStudioDir%\Code Snippets\Visual C#\My Code Snippets\"))
