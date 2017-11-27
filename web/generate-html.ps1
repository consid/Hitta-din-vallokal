& "$env:windir\system32\chcp.com" 65001

function generate
{

    $FileContent = Get-ChildItem $args[0] |Get-Content
    $NewFileContent = @()

    for ($i = 0; $i -lt $FileContent.Length; $i++) {
        if ($FileContent[$i] -like "*header.php*") {
            $NewFileContent += $HeaderContent.Replace("RostaVersionString", $Version)
        } elseif ($FileContent[$i] -like "*footer.php*") {
            $NewFileContent += $FooterContent.Replace("RostaVersionString", $Version)
        } else {
            $NewFileContent += $FileContent[$i].Replace(".php", ".html").Replace("RostaVersionString", $Version)
        }
    }

    $NewFileContent | Out-File -Encoding utf8 $args[0].ToString().Replace(".php", ".html")
}

#Change to next versionnumber before deploy
$Version = "ver11"

$HeaderContent = Get-ChildItem "header.php" |Get-Content -Encoding UTF8
$FooterContent = Get-ChildItem "footer.php" |Get-Content -Encoding UTF8


generate "index.php"
generate "fortidsrosta.php"
generate "valdagen.php"
