dotnet.exe publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true
mv .\bin\Release\net5.0\win-x64\publish\coinmanager.exe .\exe\
