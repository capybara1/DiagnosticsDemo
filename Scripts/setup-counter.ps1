<#
    .SYNOPSIS
        Demo for creating a performance counter
    .DESCRIPTION
        Demonstrates the usage of the .NET classes.
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