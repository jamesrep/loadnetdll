#####################################################
# Just a simple script for encoding/decoding base64
#
# Usage: .\encodeb64.ps1 -fileName c:\test\fil.exe
# 
#####################################################

param($fileName=$null, [bool] $decode=$false)

function encode()
{
	$fileContent = [io.file]::ReadAllBytes($fileName)
	$fileContentEncoded = [System.Convert]::ToBase64String($fileContent)
	$fileContentEncoded | set-content ($fileName + ".txt")
}


function decode()
{
	$fileContent = get-content $fileName
	$fileContentDecoded = [System.Convert]::FromBase64String($fileContent)
	[io.file]::WriteAllBytes("$fileName.decoded", $fileContentDecoded)
}


if($fileName -eq $null)
{
	write-host "[-] You must provide a proper file name"
	exit 
}

if($decode)
{
	write-host "[+] Decoding: $fileName"
	decode
}
else
{
	write-host "[+] Encoding: $fileName"
	encode
}