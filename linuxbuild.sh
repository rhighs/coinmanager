#!/bin/bash
dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true;
mv ./bin/Release/net5.0/linux-x64/publish/coinmanager ./exe/
