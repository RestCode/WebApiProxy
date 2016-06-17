function WebApiProxy-Generate-CSharp {

	$project = Get-Project
	$projectPath = [System.IO.Path]::GetDirectoryName($project.FullName)
	$root = (Join-Path $projectPath "WebApiProxy\").ToString()
	$taskPath = ($project.Object.References | where {$_.Identity -eq 'WebApiProxy.Tasks'} | Select-Object -first 1).Path

	$generateJob = Start-Job -ScriptBlock { 
        param($root,$taskPath) 

		Add-Type -Path $taskPath

		$task = New-Object WebApiProxy.Tasks.ProxyGenerationTask
		$task.Root = $root
		$task.Execute()

	 } -ArgumentList @($root,$taskPath)
	 
    $result = Receive-Job -Job $generateJob -Wait
    Write-Host $result
    Write-Host "Done."
}

Export-ModuleMember "WebApiProxy-Generate-CSharp"
