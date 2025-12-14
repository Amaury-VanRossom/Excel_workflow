## Vereisten
- .NET SDK 9.0
Controleer de versie:
    dotnet --version

## Project lokaal runnen
Ga naar de map waar het .csproj bestand staat:
    cd path/to/project

Start de development server:
    dotnet run

De applicatie draait op:
    http://localhost:5000
    https://localhost:5001

## Productie-build maken
Publiceer de app:
    dotnet publish -c Release

De build staat in:
    bin/Release/net9.0/publish/wwwroot
