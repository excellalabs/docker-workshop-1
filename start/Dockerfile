FROM microsoft/aspnetcore-build:latest

EXPOSE 80/tcp

ENV ASPNETCORE_URLS http://*:80

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "watch", "run"]