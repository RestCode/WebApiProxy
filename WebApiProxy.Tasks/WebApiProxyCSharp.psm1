function WebApiProxy-Generate-CSharp() {

	$project = Get-Project
    	$projectPath = [System.IO.Path]::GetDirectoryName($project.FullName)
	$root = (Join-Path $projectPath "WebApiProxy\")
	$rootSpaces = "$root"
	$taskPath = ($project.Object.References | where {$_.Identity -eq 'WebApiProxy.Tasks'} | Select-Object -first 1).Path


	$generateJob = Start-Job -ScriptBlock { 
        param($project,$projectPath,$rootSpaces,$taskPath) 

		Add-Type -Path $taskPath


		$config = [WebApiProxy.Tasks.Models.Configuration]::Load($rootSpaces);

		$generator = New-Object WebApiProxy.Tasks.Infrastructure.CSharpGenerator -ArgumentList @($config)
		$fileName = (Join-Path $projectPath "WebApiProxy\WebApiProxy.generated.cs")
    
		Write-Host "Generating proxy code..."

		$source = $generator.Generate()
    
		$result = New-Item $fileName `
			  -ItemType "file" -Force `
			  -Value $source
    
		# $item = $project.ProjectItems.AddFromFile($fileName)
	 } -ArgumentList @($project,$projectPath,$rootSpaces,$taskPath)
	 
    $result = Receive-Job -Job $generateJob -Wait
    Write-Host $result
    Write-Host "Done."
} 

Export-ModuleMember "WebApiProxy-Generate-CSharp"
