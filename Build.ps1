$artifacts = ".\artifacts"

Write-Host "### Cleaning artifacts folder... ###"
if(Test-Path $artifacts) { Remove-Item $artifacts -Force -Recurse }

Write-Host "### dotnet clean... ###"
dotnet clean .\src\HeboTech.Voyage\HeboTech.Voyage.csproj -c Release

Write-Host "### dotnet build... ###"
dotnet build .\src\HeboTech.Voyage\HeboTech.Voyage.csproj -c Release

Write-Host "### dotnet test... ###"
dotnet test .\src\HeboTech.Voyage.Tests\HeboTech.Voyage.Tests.csproj -c Release -f net6.0

Write-Host "### dotnet pack... ###"
dotnet pack .\src\HeboTech.Voyage\HeboTech.Voyage.csproj -c Release -o $artifacts --no-build
