<#
    .SYNOPSIS
        Demo for querying a performance counter
    .DESCRIPTION
        Demonstrates the usage of PowerShell by a simple example.
    .LINKS
        https://www.codeproject.com/Articles/46390/WMI-Query-Language-by-Example
        https://mcpmag.com/articles/2018/02/07/performance-counters-in-powershell.aspx
#>

$categoryName = "DiagnosticsDemo"

If (-not [System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)) {
    Write-Host "The category $categoryName does not exists"
    Exit 1
}

$category = Get-Counter -ListSet $categoryName
$counters = $category.Counter
Get-Counter -Counter $counters -MaxSamples 20 -SampleInterval 3 | ForEach {
    $_.CounterSamples | ForEach {
        [pscustomobject]@{
            TimeStamp = $_.TimeStamp
            Path = $_.Path
            Value = $_.CookedValue
        }
    }
}