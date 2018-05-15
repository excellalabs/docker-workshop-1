## Part 2. Build your own container for an app

1. Ctrl-C to stop the container. In the start/ directory, create a new file in the source directory called `Dockerfile` (no extension). We will continue to use the sample app in this folder for Dockerizing. Put the follow into this file:

      ```
      FROM microsoft/aspnetcore-build:latest

      EXPOSE 80/tcp

      ENV ASPNETCORE_URLS http://*:80

      COPY . /app
      WORKDIR /app

      RUN ["dotnet", "restore"]
      RUN ["dotnet", "build"]
      ENTRYPOINT ["dotnet", "watch", "run"]
      ```

    This specifies what the container will have, and in this case, it's based on the public .NET Core image. Then it adds some configuration for ASP.NET, copying in source code, and building and running the app.

1. Run this command to build an image from the Dockerfile:

    `docker image build -t <AnyImageNameYouWant> .`

1. See that your image was created correct from the build step above, by listing the images on your machine, by typing `docker images`. You should see it in the list.

Then you can create new containers based off this image, which we'll do in the next part.

[Go to Part 3](README-part3.md)