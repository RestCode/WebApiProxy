param($installPath, $toolsPath, $package, $project)
  remove-item ([System.Environment]::ExpandEnvironmentVariables("%VisualStudioDir%\Code Snippets\Visual C#\My Code Snippets\webapiproxy.snippet"))