<#
    .SYNOPSIS
        Demo for creating a performance counter
    .LINKS
        https://www.msxfaq.de/code/powershell/psperfcounter.htm#performancecounter_anlegen
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountercategory.create
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountertype
    .DESCRIPTION
        Demonstrates the usage of the .NET classes.
#>

$categoryName = "DiagnosticsDemo"
$categoryHelp = "Demo for performance counters"
$counterName = "DiagnosticsDemo1"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "You must have elevated privileges on the computer to create a new performance counter."
    Exit 1
}

If ([System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)) {
    Write-Host "The category $categoryName already exists"
    Get-Counter -ListSet $categoryName
    Exit 0
}

Write-Host "Creating category $categoryName"
$counterData = New-Object System.Diagnostics.CounterCreationDataCollection
$counter = New-Object System.Diagnostics.CounterCreationData
$counter.CounterType = [System.Diagnostics.PerformanceCounterType]::NumberOfItems64
$counter.CounterName = $counterName
$counterData.Add($counter)
[System.Diagnostics.PerformanceCounterCategory]::Create( `
    $categoryName, `
    $categoryHelp, `
    [System.Diagnostics.PerformanceCounterCategoryType]::SingleInstance, `
    $counterData)

Write-Host "Exiting, execute the script a second time to use the counter. This allows time for the operating system to refresh its list of registered counters and their configuration."