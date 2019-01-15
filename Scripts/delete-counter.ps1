<#
    .SYNOPSIS
        Demo for deleting a performance counter category
    .DESCRIPTION
        Demonstrates the usage of .NET classes by a simple example.
    .LINKS
        https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountercategory.delete
#>

$categoryName = "DiagnosticsDemo"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "You must have elevated privileges on the computer to delete a category."
    Exit 1
}

If (-not [System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)) {
    Write-Host "The category $categoryName does not exist"
    Exit 0
}

Write-Host "Deleting category $categoryName"
[System.Diagnostics.PerformanceCounterCategory]::Delete($categoryName)
