cd C:\_TEMP_\DevGearbox
dotnet clean
dotnet restore  
dotnet build --no-incremental > build_result.txt 2>&1
Get-Content build_result.txt

