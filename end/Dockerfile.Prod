# Describes how to build a Docker image. Each instruction creates a new layer in the image. 

# This is good for deployed apps as it will be minimal 
FROM microsoft/aspnetcore-build:latest

# set up package cache
RUN curl -o /tmp/packagescache.tar.gz https://dist.asp.net/packagecache/aspnetcore.packagecache-1.0.1-debian.8-x64.tar.gz && \
    mkdir /packagescache && cd /packagescache && \
    tar xvf /tmp/packagescache.tar.gz && \
    rm /tmp/packagescache.tar.gz && \
    cd /

# set env var for packages cache
ENV DOTNET_HOSTING_OPTIMIZATION_CACHE /packagescache

# set up network
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
#ENV ASPNETCORE_URLS http://+:80

# copy files from current directory to target on container - i.e. if you want your app in the container and want to deploy it
COPY . /sampleAspnetCoreWebApiApp 
WORKDIR /sampleAspnetCoreWebApiApp

# RUN ["dotnet", "restore"]
# RUN ["dotnet", "build"]
# ENTRYPOINT ["dotnet", "watch", "run"]