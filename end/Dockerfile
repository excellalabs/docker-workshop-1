# Describes how to build a Docker image. Each instruction creates a new layer in the image. 

# base image - avoid :lastest as it can upgrade versions automatically 
FROM microsoft/aspnetcore-build:latest
# This is good for deployed apps as it will be minimal, and no SDK 
# FROM microsoft/dotnet:1.0.1-core 

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:5000

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
ENTRYPOINT ["dotnet", "watch", "run"]

##################################### REFERENCE: #################################################
# USAGE - build image, and expose ports, map the current app folder: 

#   docker build -t aspnetcore-development-env .
#		docker run -it -p 5000:5000 -v $(pwd):/app -t aspnetcore-development-env

# FOR MULTIPLE CONTAINERS

# Option 1
# Start PostgreSQL and ASP.NET Core (link ASP.NET core to ProgreSQL container with legacy linking)
 
# docker run -d --name my-postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d -p 5000:5000 --link my-postgres:postgres [yourDockerHubID]/dotnet:1.0.0

# Option 2: Create a custom bridge network and add containers into it

# docker network create --driver bridge isolated_network
# docker run -d --net=isolated_network --name postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d --net=isolated_network --name aspnetcoreapp -p 5000:5000 [yourDockerHubID]/dotnet:1.0.0