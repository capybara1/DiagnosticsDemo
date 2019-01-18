<#
    .SYNOPSIS
        Demo for creating a performance counter
    .LINKS
        https://www.msxfaq.de/code/powershell/psperfcounter.htm#performancecounter_anlegen
        https://manski.net/2013/11/windows-performance-counter-types/
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountercategory.create
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountertype
    .DESCRIPTION
        Demonstrates the usage of the .NET classes.
#>

$categoryName = "DiagnosticsDemo"
$categoryHelp = "Demo for performance counters"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "You must have elevated privileges on the computer to create a new performance counter."
    Exit 1
}

If ([System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)) {
    Write-Host "The category $categoryName already exists"
    Get-Counter -ListSet $categoryName
    Exit 0
}

$types = @(
    @{ Main = "NumberOfItems64" },
    @{ Main = "CounterDelta64" },
    @{ Main = "CounterTimer" },
    @{ Main = "CounterTimerInverse" },
    @{ Main = "CountPerTimeInterval64" },
    @{ Main = "RateOfCountsPerSecond64" },
    @{ Main = "ElapsedTime" },
    @{ Main = "RawFraction"; Base = "RawBase" },
    @{ Main = "SampleFraction"; Base = "SampleBase" },
    @{ Main = "AverageCount64"; Base = "AverageBase" },
    @{ Main = "AverageTimer32"; Base = "AverageBase" }
)

Write-Host "Creating category $categoryName"
$counters = New-Object System.Diagnostics.CounterCreationDataCollection
ForEach ($type in $types) {
    $counter = New-Object System.Diagnostics.CounterCreationData
    $counter.CounterType = [System.Diagnostics.PerformanceCounterType]$type.Main
    $counter.CounterName = $type.Main
    $counters.Add($counter) | Out-Null
    If ($type.ContainsKey("Base")) {
        $counter = New-Object System.Diagnostics.CounterCreationData
        $counter.CounterType = [System.Diagnostics.PerformanceCounterType]$type.Base
        $counter.CounterName = $type.Main + "Base"
        $counters.Add($counter) | Out-Null
    }
}
[System.Diagnostics.PerformanceCounterCategory]::Create(
    $categoryName,
    $categoryHelp,
    [System.Diagnostics.PerformanceCounterCategoryType]::SingleInstance,
    $counters)

Write-Host "Exiting, execute the script a second time to use the counter. This allows time for the operating system to refresh its list of registered counters and their configuration."