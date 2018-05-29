FROM microsoft/aspnetcore-build:2.0

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:80

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
