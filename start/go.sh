#!bin/bash
set -e
dotnet restore
dotnet watch run
# dotnet test test/WebTests/project.json
# rm -rf $(pwd)/publish/web
# dotnet publish src/Web/project.json -c release -o $(pwd)/publish/web