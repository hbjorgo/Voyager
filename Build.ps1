$artifacts = ".\artifacts"

Write-Host "### Cleaning artifacts folder... ###"
if(Test-Path $artifacts) { Remove-Item $artifacts -Force -Recurse }

Write-Host "### dotnet clean... ###"
dotnet clean .\src\HeboTech.Voyager\HeboTech.Voyager.csproj -c Release

Write-Host "### dotnet build... ###"
dotnet build .\src\HeboTech.Voyager\HeboTech.Voyager.csproj -c Release

Write-Host "### dotnet test... ###"
dotnet test .\src\HeboTech.Voyager.Tests\HeboTech.Voyager.Tests.csproj -c Release -f net6.0

Write-Host "### dotnet pack... ###"
dotnet pack .\src\HeboTech.Voyager\HeboTech.Voyager.csproj -c Release -o $artifacts --no-build
