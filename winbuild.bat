dotnet.exe publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true /p:TargetPlatform=net5.0-windows
move .\bin\Release\net5.0-windows\win-x64\publish\coinmanager.exe .\exe\
