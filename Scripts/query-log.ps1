<#
    .SYNOPSIS
        Demo for querying a Windows Event-Log
    .DESCRIPTION
        Demonstrates the usage of the wevtutil command by a simple example.
    .LINKS
        https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/wevtutil
    .NOTES
        Use the /r or the /remote parameter to access remote computers
#>

Function PrettyPrint-Xml ([xml]$xml)
{
    $stringWriter = New-Object System.IO.StringWriter;
    $xmlWriter = New-Object System.Xml.XmlTextWriter $stringWriter;
    $xmlWriter.Formatting = "indented";
    $xml.WriteTo($xmlWriter);
    $xmlWriter.Flush();
    $stringWriter.Flush();
    Write-Output $stringWriter.ToString();
}

Write-Host "Last time the event log service was shut down which usually indicates a system restart or shut down"
PrettyPrint-Xml(& wevtutil query-events System "/q:*[System[(EventID=6006)]]" /rd:true /f:xml /c:1)
