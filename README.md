# Running in a Docker container

This is a getting started workshop for using Docker, and focuses on using it for development since that is a logical and no-risk place to start. We will use an ASP.NET Core app for the exercise, but any app on most stacks that can run on Linux should work. You will be guided through running a container locally from an image in Docker Hub, then putting your own app into a container for it building locally and running it.

Here's an accompanying [slide deck](https://www.slideshare.net/wynvandevanter/developer-workflow-with-docker-75189136) and [course outline](OUTLINE.md), for delivering this workshop to a group.

## Preparation

Please go through these steps before the workshop, to ensure you don't spend time with setup, and downloading a large Docker image.

1. **Install Docker & Run a Container** Follow Docker's **installation instructions** for your platform, including the section for **testing it** to ensure it's working.

    - [Install & set up for Windows](https://store.docker.com/editions/community/docker-ce-desktop-windows?tab=description)

    - [Install & set up for Mac](https://store.docker.com/editions/community/docker-ce-desktop-mac?tab=description)

1. **On Windows only**, you will have to share your drive. You can do this by:

    - Right-clicking the docker tray icon
    - Selecting `Settings...`
    - On the left-hand side, select `Shared Drives`
    - Check the appropriate drive and click `Apply`
    - Enter your user account's password and click `OK`.

1. Try running the offical Jenkins container to see how easy it is to start a container with a service, using Docker. It will download the image from Docker Hub if it doesn't find it already in your local Docker repo:

    `docker run -d -p 49001:8080 -v $PWD/jenkins:/var/jenkins_home:z -t jenkins`

1. Download the ASP.NET Core development Docker image (wi-fi because it's large), by running this from your terminal:

    `docker pull microsoft/aspnetcore-build`

## Part 1. Run source code in a container for development work

1. Clone this repo for the workshop somewhere, and go into the **start/** directory.

1. To run an existing container from a Docker Hub image, to try out running a container, run this Docker command from your command line:

    - Mac/Linux:  `docker run -it -p 5000:5000 -v $(pwd):/app -t wyntuition/aspnetcore-development-env`
    - Windows: `docker run -it -p 5000:5000 -v /C/path/to/repo/docker-workshop-1/start:/app -t wyntuition/aspnetcore-development-env`

1. Now you can change your source code, and the container will rebuild and run the code when you save changes. Open the source directory with your favorite IDE and try it.

## Part 2. Build your own container for an app

1. Ctrl-C to stop the container. In the start/ directory, create a new file in the source directory called `Dockerfile` (no extension). We will continue to use the sample app in this folder for Dockerizing. Put the follow into this file:

      ```
      FROM microsoft/dotnet:1.0.1-sdk-projectjson

      EXPOSE 5000/tcp

      ENV ASPNETCORE_URLS http://*:5000

      COPY . /app 
      WORKDIR /app

      RUN ["dotnet", "restore"]
      RUN ["dotnet", "build"]
      ENTRYPOINT ["dotnet", "watch", "run"]
      ```

    This specifies what the container will have, and in this case, it's based on the public .NET Core image. Then it adds some configuration for ASP.NET, copying in source code, and building and running the app.

1. Run this command to build an image from the Dockerfile:

    `docker build -t <AnyImageNameYouWant> .`

1. See that your image was created correct from the build step above, by listing the images on your machine, by typing `docker images`. You should see it in the list.

Then you can create new containers based off this image, which we'll do in the next part.

## Part 3: Run the container

Use the following docker run command, specifying a port binding for listening, the current app folder to mount in the container, and the image name, using the following command.

1. Go to your ASP.NET Core app's directory (or an empty directory for a new app)

    `docker run -it -p 5000:5000 -v $(pwd):/app -t <yourTag:YourAspNetImageName>`

    Now you can code in your host environment using your IDE as usual, and the container will receive any file changes since your application directory is mounted into the container. 

1. Check that the app is running and accessible by browsing to [http://localhost:5000/api/helloworld](http://localhost:5000/api/helloworld)

## Part 4. Docker Compose

You can use Docker Compose to spin up multiple containers at once, in order to create a multi-server environmnet. The most common example would be to spin up a container running the application, and a container running the database.

We're going to create a `docker-compose.yml` file in the repo that use Docker Compose to spin up an ASP.NET Core container again, but also a postgreSQL container that it will be able to talk to. It will automatically look for, find and use the official PostgreSQL image on Docker Hub. When you don't specific a image repo, it will by default check Docker Hub.

The app will talk to the container with the database engine, use a volume to map the database data file onto the host (so it persists even if the container doesn't), and see there is no database, and create one.

1. Create a file in the root of your app source called `docker-compose.yml` and add the following:

```
version: '2'

services: # these are all the services that a docker app uses

  web: # this is the name of the service we're creating; it's chosen by us. Here, we're calling it "web". 
    container_name: 'aspnetcore-from-compose' # this is the name of the container to us
    image: 'aspnetcore-from-compose' 
    build:
      context: .
      dockerfile: Dockerfile
    volumes:
      - .:/app
    entrypoint: ["sh", "./go.sh"] 
    ports:
    - "5000:5000"
    depends_on:
    - "postgres" # this makes sure that the postgres service below has been started prior to attempting to start this service. 
    networks:
      - app-network # this is a docker feature to allow you to place your various services within a virtual network so they can talk to each other. Note all the services we define here use the "app-network" network.

  postgres:
    container_name: 'postgres-from-compose'
    image: postgres
    environment:
      POSTGRES_PASSWORD: password
    networks:
      - app-network
    volumes:
      - 'postgres:/var/lib/postgresql/data'

networks:
  app-network:
    driver: bridge

volumes:
  postgres: {}
```

1. Modify your Dockerfile to remove these (last 3) lines. We are defining this in docker-compose now, and calling a script:

  ```
  RUN ["dotnet", "restore"]
  RUN ["dotnet", "build"]
  ENTRYPOINT ["dotnet", "watch", "run"]
  ```

2. Build the images and run them by running this from the command line:

    `docker-compose up`

1. Windows users, if you get an error referencing the `go.sh` file, it likely has Windows file endings, but needs them to be Linux. You can convert it by opening git bash, and running `dos2unix </path/To/File/go.sh>`

1. Check that your app is running and accessible at [http://localhost:5000/api/articles](http://localhost:5000/api/articles).

1. Now that you have a databasec contianer running, you can add some data and then query it. Run this to add some then reload the URL,

    `curl -H "Content-Type: application/json" -X POST -d '{"title":"I Was Posted"}' http://localhost:5000/api/articles`
    
    **Windows Note**: On Windows in Powershell, `curl.exe` is not included and `curl` is actually mapped to a powershell command called `Invoke-WebRequest`, so it won't have the same behavior. The correct syntax in this case is `curl -Headers @{"Content-Type" = "application/json"} -Uri http://localhost:5000/api/articles -Method POST -Body @{"title" = "I Was Posted"}`

1. Stop the containers: when you run a container(s) with docker or docker-compose in the foreground (i.e. without the -d flag), Ctrl-C will stop them. If you run them in the background, you can use `docker-compose stop`. If you need to rebuild the image because you changed the Dockerfile/etc, you can use `docker-compose up --build`. If you want to remove the containers it creates, you can use `docker-compose down`.
 
1. You should be able to develop as usual on your computer, using the full development workflow with Docker with a real-world-like app. Go ahead and try changing the source code again and saving, and see the app update.

## Part 5. Clean up

It's important to clean up unused stopped containers, old images, etc.

- Remove all stopped containers:

  `docker rm $(docker rm -a)`

- Remove all dangling images:

  `docker rmi $(docker images -f dangling=true)`

- Remove all unused containers, volumes, networks and dangling images (add -a to remove any unreferenced images as well):

  `docker system prune`

## Useful commands

- List your containers (-a shows stopped ones too):

  `docker ps -a`

- List your images: 

  `docker images`

- It is useful to log into your containers sometimes. To do that, use this:

  `docker exec -ti <container ID or name> sh`

- Check the logs from your containers:

  `docker logs <container ID or name>`

- Inspect your container(s):

  `docker inspect <container ID or name>`

- View and manage volumes,

  - `docker volume ls`
  - `docker volume inspect <name>`

- View and manage networks,

  - `docker network ls`
  - `docker network inspect <name>`

- Remove stopped containers:

  - `docker rm $(docker ps -a q)`

- Remove all unused containers, volumes, networks and images (both dangling and unreferenced):

  - `docker system prune` (add -a to remomve unnused images, not just dangling)
