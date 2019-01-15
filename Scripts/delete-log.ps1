<#
    .SYNOPSIS
        Demo for deleting a Windows Event-Log Source
    .DESCRIPTION
        Demonstrates the usage of .NET classes by a simple example.
        See also https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.eventlog.deleteeventsource
    .NOTES
        For removing a single source from a log use
        [System.Diagnostics.EventLog]::SourceExists($source) and
        [System.Diagnostics.EventLog]::DeleteEventSource($source)
#>

$logName = "DiagnosticsDemo"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "You must have elevated privileges on the computer to delete an event source."
    Exit 1
}

if (-not [System.Diagnostics.EventLog]::Exists($logName)) {
    Write-Host "The log $logName does not exist"
    Exit 0
}

Write-Host "Deleting log $logName"
[System.Diagnostics.EventLog]::Delete($logName);
