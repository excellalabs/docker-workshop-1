# Running in a Docker container 

We are going to go through using Docker in the devleopment process. We will use an ASP.NET Core app for the exercise, but any app on most stacks should work.

Objectives: 
- Get Docker installed and running locally 
- Run a container from Docker Hub
- Create a container for building and running the provided sample app locally. The source code will be mounted into the container.
- Make code changes and go through the development workflow with a container
- Use Compose to run multiple containers easily 

## Part 1. Install & Run Docker 

1. Follow Docker's **installation instructions** for your platform, including the section for **testing it** to ensure it's working.

    - [Install & set up for Windows](https://store.docker.com/editions/community/docker-ce-desktop-windows?tab=description)

    - [Install & set up for Mac](https://store.docker.com/editions/community/docker-ce-desktop-mac?tab=description)

1. To run an existing container from a Docker Hub image, to try out running a container, run this Docker command from your command line:

    1. In your command line, go to the root directory of some source code.
    1. Run:
      
        `docker run -it -p 5000:5000 -v $(pwd):/app -t wyntuition/aspnetcore-development-env`

    1. Now you can change your source code, and the container will rebuild and run the app when you save changes. 

## Part 2. Build your own container for an app

1. Create a new file in the root of your source directory, called `Dockerfile` (no extension). 

    Take a look at the Dockerfile in the repo's end folder. This specifies what the container will have, and in this case, it's based on the public .NET Core image. Then it adds some configuration for ASP.NET. You can build the ASP.NET Core container from the provided Dockerfile, following these steps. 

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

1. Copy the contents into your `Dockerfile`, and run this command in the same directory as the file:

    ```docker build -t <yourTag:YourAspNetImageName> .```

1. See that your image was created correct from the build step above, by listing the images on your machine, by typing `docker images`. You should see it in the list.

Then you can create new containers based off this image, which we'll do in the next step. Note, your application code will be in this container. 

## Part 3: Run the container

Use the following docker run command, specifying a port binding for listening, the current app folder to mount in the container, and the image name, using the following command.

1. Go to your ASP.NET Core app's directory (or an empty directory for a new app)

    `docker run -d -p 8080:5000 -v $(pwd):/app -t <yourTag:YourAspNetImageName>`

    Now you can code in your host environment using your IDE as usual, and the container will receive any file changes since your application directory is mounted into the container. 

1. Check that the app is running and accessible.

## Part 4. Docker Compose

You can use Docker Compose to spin up multiple containers at once, in order to create a multi-server environmnet. The most common example would be to spin up a container running the application, and a container running the database.

There is a `docker-compose.yml` file in the repo that use Docker Compose to spin up an ASP.NET Core container, and a postgreSQL container.

1. Create a file in the root of your app source called `docker.compose.yml`

    ```
    version: '3'

    services:

      web:
        container_name: 'aspnetcore-from-compose'
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
        - "postgres"
        networks:
          - app-network

      postgres:
        container_name: 'postgres-from-compose'
        image: postgres
        environment:
          POSTGRES_PASSWORD: pgPassword
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

1. Modify your Dockerfile to remove the last 3 lines, the 2 that run commands, and the entrypoint. We are defining this in docker-compose now, and calling a script. 

1. Build the images and run them by running this from the command line:

    `docker-compose up`

    You can add -d at the end so the containers run in the background; without it they would stop when you exit the shell. You can connect to the shell via 'docker exec -ti <Container> sh`.

1. Check that your app is running and accessible. 

1. Now that you have a databasec contianer running, you can add some data and then query it. Run this to add some, 

    `curl -H "Content-Type: application/json" -X POST -d '{"title":"I Was Posted"}' http://localhost:5000/api/articles`
    
1. Stop the containers:

    `docker-compose stop`

    With them running, you should be able to navigate to your web app (or the Web API sample endpoint in this app - http://localhost:8080/api/articles). You should be able to develop as usual on your computer, but when you save, your code is rebuilt in the ASP.NET container, and then run from there. You can try changing the /Controllers/ArticlesController.cs code and see it update at that endpoint, which is being hosted from the ASP.NET container.

## Part 5. Clean up

- `docker rm $(docker rm -a)`
- `docker rmi $(docker images -f dangling=true)`
- `docker system prune`

## Useful commands

- It is useful to log into your containers. To do that, use this: 

  `docker exec -ti <Container name> sh`

- Check the logs from your containers: 

    1. Type `docker ps -a` to get the name or ID of your container
    1. Type  `docker logs <container ID or name>`

- Inspect your container(s): 

  `docker inspect <container ID/name>`

- View and manage volumes, 

  - `docker volume ls`
  - `docker volume inspect <name>`

- View and manage networks, 

  - `docker network ls` 
  - `docker network inspect <name>`