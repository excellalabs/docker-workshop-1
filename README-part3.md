## Part 3: Run the container

Use the following docker run command, specifying a port binding for listening, the current app folder to mount in the container, and the image name, using the following command.

1. Go to your ASP.NET Core app's directory (or an empty directory for a new app)

    `docker container run -it -p 80:80 -v $(pwd):/app -t <yourTag:YourAspNetImageName>`

    Windows:

    `docker container run -it -p 80:80 -v /C/<PATH TO CLONE docker-workshop-1>/start:/app -t <yourTag:YourAspNetImageName>`

    Now you can code in your host environment using your IDE as usual, and the container will receive any file changes since your application directory is mounted into the container. 

1. Check that the app is running and accessible by browsing to [http://localhost/api/values](http://localhost/api/helloworld)

[Go to Part 4](README-part4.md)