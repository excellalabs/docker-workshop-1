# Running in a Docker container

This is a getting started workshop for using Docker, and focuses on using it for development since that is a logical and no-risk place to start. We will use an ASP.NET Core app for the exercise, but any app on most stacks that can run on Linux should work. You will be guided through running a container from an image from Docker Hub. Then you'll create your own image & container, in order to build and run an app inside the container, without having to install anything locally except Docker.

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

1. **Test the installation by running the offical Jenkins container**. It will download the image from Docker Hub if it doesn't find it already in your local Docker repo:

    `docker run -d -p 49001:8080 -v $PWD/jenkins:/var/jenkins_home:z -t jenkins`

1. **Download the ASP.NET Core development Docker image** (wi-fi because it's large), by running this from your terminal:

    `docker pull microsoft/aspnetcore-build`

[Get started!](README-part1.md)

