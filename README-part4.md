## Part 4. Docker Compose

You can use Docker Compose to spin up multiple containers at once, in order to create a multi-server environmnet. The most common example would be to spin up a container running the application, and a container running the database.

We're going to create a `docker-compose.yml` file in the repo that use Docker Compose to spin up an ASP.NET Core container again, but also a postgreSQL container that it will be able to talk to. It will automatically look for, find and use the official PostgreSQL image on Docker Hub. When you don't specific a image repo, it will by default check Docker Hub.

The app will talk to the container with the database engine, use a volume to map the database data file onto the host (so it persists even if the container doesn't), and see there is no database, and create one.

1. Create a file in the root of your app source called `docker-compose.yml` and add the following:

    ```
    version: '2'

    services: # these are all the services that a docker app uses

      # this is the name of the service we're creating; it's chosen by us. Here, we're calling it "web"
      web: 
        container_name: 'aspnetcore-from-compose' # this is the name of the container to us
        image: 'aspnetcore-from-compose'
        build:
          context: .
          dockerfile: Dockerfile
        volumes:
          - .:/app
        ports:
        - "80:80"
        depends_on:
        - "postgres"
        # this is a docker feature to allow you to place your various services within a virtual network 
        # so they can talk to each other. All the services we define here use the "app-network" network.
        networks:
          - app-network

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

1. Build the images and run them by running this from the command line:

    `docker-compose up`

1. Check that your app is running and accessible at [http://localhost/api/values](http://localhost/api/articles).

1. Stop the containers: when you run a container(s) with docker or docker-compose in the foreground (i.e. without the -d flag), Ctrl-C will stop them. If you run them in the background, you can use `docker-compose stop`. If you need to rebuild the image because you changed the Dockerfile/etc, you can use `docker-compose up --build`. If you want to remove the containers it creates, you can use `docker-compose down`.
 
1. You should be able to develop as usual on your computer, using the full development workflow with Docker with a real-world-like app. Go ahead and try changing the source code again and saving, and see the app update.

[Go to Part 5](README-part5.md)