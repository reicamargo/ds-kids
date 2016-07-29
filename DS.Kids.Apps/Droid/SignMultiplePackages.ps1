function EnsurePsbuildInstlled
{
    [cmdletbinding()]
    param(
        [string]$psbuildInstallUri = 'https://raw.githubusercontent.com/ligershark/psbuild/master/src/GetPSBuild.ps1'
    )
    process{
        if(-not (Get-Command "Invoke-MsBuild" -errorAction SilentlyContinue)){
            'Installing psbuild from [{0}]' -f $psbuildInstallUri | Write-Verbose
            (new-object Net.WebClient).DownloadString($psbuildInstallUri) | iex
        }
        else{
            'psbuild already loaded, skipping download' | Write-Verbose
        }

        # make sure it's loaded and throw if not]
        if(-not (Get-Command "Invoke-MsBuild" -errorAction SilentlyContinue)){
            throw ('Unable to install/load psbuild from [{0}]' -f $psbuildInstallUri)
        }
    }
}

#CODE

Write-Host "---> POS-BUILD : RELEASE SIGN PACKAGES <---"

EnsurePsbuildInstlled

Invoke-MSBuild .\DS.Kids.Apps.Droid.csproj -Configuration Release -Targets Clean
Invoke-MSBuild .\DS.Kids.Apps.Droid.csproj -Configuration Release -Targets PackageForAndroid

$zipalign     = $($env:LOCALAPPDATA + '\Android\android-sdk\build-tools\23.0.1\zipalign.exe')
$jarsigner    = 'C:\Program Files (x86)\Java\jdk1.7.0_71\bin\jarsigner.exe'
$keystore     = '.\dskids.keystore'
$apk          = '.\bin\Release\br.com.dskids'
$DSKidsDeploy = '.\DSKidsDeploy'

$securestorepass = Read-Host 'Entre com a StorePass' -AsSecureString #123Mudar
$storepass = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($securestorepass))

& $jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore $keystore -storepass $storepass -signedjar $($apk + '-armeabi-Signed.apk')     $($apk + '-armeabi.apk') DSKIDS
& $jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore $keystore -storepass $storepass -signedjar $($apk + '-armeabi-v7a-Signed.apk') $($apk + '-armeabi-v7a.apk') DSKIDS
& $jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore $keystore -storepass $storepass -signedjar $($apk + '-x86-Signed.apk')         $($apk + '-x86.apk') DSKIDS

# Now zipalign it.  The -v parameter tells zipalign to verify the APK afterwards.
& $zipalign -f -v 4 $($apk + '-armeabi-Signed.apk')      $($apk + '-armeabi-Aligned.apk')
& $zipalign -f -v 4 $($apk + '-armeabi-v7a-Signed.apk')  $($apk + '-armeabi-v7a-Aligned.apk')
& $zipalign -f -v 4 $($apk + '-x86-Signed.apk')          $($apk + '-x86-Aligned.apk') 


if(-not (Test-Path($DSKidsDeploy)))
{
    New-Item $DSKidsDeploy -type directory
}
 
Copy-Item $($apk + '-armeabi-Aligned.apk')     $($DSKidsDeploy + '\br.com.dskids-armeabi-Aligned.apk')     -Force
Copy-Item $($apk + '-armeabi-v7a-Aligned.apk') $($DSKidsDeploy + '\br.com.dskids-armeabi-v7a-Aligned.apk') -Force
Copy-Item $($apk + '-x86-Aligned.apk')         $($DSKidsDeploy + '\br.com.dskids-x86-Aligned.apk')         -Force