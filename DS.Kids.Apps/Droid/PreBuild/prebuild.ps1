# CONSTANTS
$crashlyticsStringName = 'com.crashlytics.android.build_id'


#CODE
[string]$manifest = $args[0]
[string]$resourceStrings = $args[1]

Write-Host "---> PRE-BUILD : Crashlytics Configuration <---"
Write-Host "Manifest File: $manifest"
Write-Host "Resource String File: $resourceStrings"

try
{
    [xml]$manifestFile = Get-Content $manifest
    [xml]$resourceStringsFile = Get-Content $resourceStrings

    $node = $resourceStringsFile.resources.string | where {$_.name -eq $crashlyticsStringName}
    if(!$node)
    {
        Write-Host "PRE-BUILD(Crashlytics): <string name='com.crashlytics.android.build_id'></string> não encontrado no arquivo '$resourceStrings'. Gerando..."

        $node = $resourceStringsFile.CreateElement("string")
        $node.SetAttribute("name", $crashlyticsStringName);
        $resourceStringsFile.resources.AppendChild($node)
    }
    $node.set_InnerXML($manifestFile.manifest.versionName)

    Write-Host "VersionName: $manifestFile.manifest.versionName"

    $resourceStringsFile.Save($resourceStrings)

    Write-Host "File '$resourceStrings' Saved"
}
catch
{
    Write-Host $_.Exception.Message
    exit -1
}