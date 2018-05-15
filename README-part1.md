## Part 1. Run source code in a container for development work

You can build and run an app without installing anything other than Docker, so let's illustrate that with the hello world one provided.

1. Clone this repo for the workshop somewhere, and go into the **start/** directory, which has a hello world app.

1. To run the source code inside an existing container from a Docker Hub image, run this Docker command from your command line:

  *Mac/Linux*:

  Build & publish
  ```bash
  docker container run -it -v $(pwd):/app --workdir /app microsoft/aspnetcore-build bash -c "dotnet restore && dotnet publish -c Release -o ./bin/Release/PublishOutput"
  ```

  Run app
  ```bash
  docker container run -it -p 80:80 -v $(pwd):/app --workdir /app -t microsoft/aspnetcore-build bash -c "dotnet run"
  ```

  *Windows*:

  Build and publish

  ```bash
  docker container run -it -v /C/<PATH TO CLONE docker-workshop-1>/start:/app --workdir /app microsoft/aspnetcore-build bash -c "dotnet restore && dotnet publish -c Release -o ./bin/Release/PublishOutput"
  ```

  Run app
  ```bash
    docker container run -it -p 80:80 -v /C/<PATH TO CLONE docker-workshop-1>/start:/app --workdir /app -t microsoft/aspnetcore-build bash -c "dotnet run"
  ```

1. Now you can navigate to the app at [http://localhost/api/values](http://localhost/api/values).

1. You can change the source code, and the container will rebuild and run the code when you save changes. Open the source directory with your favorite IDE and try it - change the Controllers/ValuesController Get() method to return something else, save it, then relead the browser when it's finished rebuilding per the console.

[Go to Part 2](README-part2.md)