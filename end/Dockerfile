FROM microsoft/aspnetcore-build:latest

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:5000

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "watch", "run"]