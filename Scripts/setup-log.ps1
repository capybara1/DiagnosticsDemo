<#
    .SYNOPSIS
        Demo for creating and modifying a Windows Event-Log Source
    .DESCRIPTION
        Demonstrates the usage of the .NET Classes and the wevtutil command by a simple example.
        See also https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.eventlog.createeventsource
        See also https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/wevtutil
    .NOTES
        If $logName is null or an empty string, the log defaults to the Application log.
        If the log does not exist on the local computer, the system creates a custom log
        and registers the $source as a source for that log.
#>

$source = "DiagnosticsDemo"


$logName = "DiagnosticsDemo"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "You must have elevated privileges on the computer to create a new event source."
    Exit 1
}

if (-not [System.Diagnostics.EventLog]::SourceExists($source)) {
    Write-Host "Creating source $source for log $logName"
    [System.Diagnostics.EventLog]::CreateEventSource($source, $logName)
    
    Write-Host "Exiting, execute the script a second time to use the source. This allows time for the operating system to refresh its list of registered event sources and their configuration."
    Exit 0
}

Write-Host "The source $source already exists"
& wevtutil get-log $logName