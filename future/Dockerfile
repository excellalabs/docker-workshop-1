# Describes how to build a Docker image. Each instruction creates a new layer in the image. 

# base image - avoid :lastest except in experiments as it can upgrade versions automatically 
FROM microsoft/aspnetcore-build:latest
# This is good for deployed apps as it will be minimal, and no SDK 
# FROM microsoft/dotnet:1.0.1-core 

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:5000

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "run"]