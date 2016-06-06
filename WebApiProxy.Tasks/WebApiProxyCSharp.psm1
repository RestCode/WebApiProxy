function WebApiProxy-Generate-CSharp($endpoint) {

	$project = Get-Project
	$projectPath = [System.IO.Path]::GetDirectoryName($project.FullName)
	$root = (Join-Path $projectPath "WebApiProxy\")
	$rootSpaces = "$root"
	$taskPath = ($project.Object.References | where {$_.Identity -eq 'WebApiProxy.Tasks'} | Select-Object -first 1).Path


	$generateJob = Start-Job -ScriptBlock { 
        param($project,$projectPath,$rootSpaces,$taskPath) 

		Add-Type -Path $taskPath


		$config = [WebApiProxy.Tasks.Models.Configuration]::Load($rootSpaces);
        if ($endpoint){
            $config.Endpoint = $endpoint 
        }

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

function ApiProxy-Generate($serviceName)
{
    $consulHost = "http://localhost:8500"
    $traefikInfo = iwr -Uri "${consulHost}/v1/health/service/traefik?passing" | ConvertFrom-Json
    
    $endpoint = getAddress -infos $traefikInfo -serviceName "traefik" 
    
    if (-not $endpoint){
        $serviceInfo = iwr -Uri "${consulHost}/v1/health/service/${serviceName}" | ConvertFrom-Json
        $endpoint = getAddress -infos $serviceInfo -serviceName $serviceName
    }

    $endpoint = "http://$endpoint/api/proxies/"
    Write-Host "generate from $endpoint"
	WebApiProxy-Generate-CSharp $endpoint
}

function getAddress($infos,$serviceName){
    $address = ""
    foreach ($info in $infos)
    {
        $isCheckOk = $false
        foreach($check in $info.Checks)
        {
            if($check.Name -eq $serviceName -and $check.Status -eq "passing")
            {
                $isCheckOk = $true
            }
        }

        if($isCheckOk){
            $address = "$($info.Service.Address):$($info.Service.Port)"
        }
    }
    $address
}

Export-ModuleMember "WebApiProxy-Generate-CSharp","ApiProxy-Generate"
