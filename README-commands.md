## Useful commands

- List your containers (-a shows stopped ones too):

  `docker container ls -a` (docker ps is deprecated)

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

  - `docker container rm $(docker ps -a q)`

- Remove all unused containers, volumes, networks and images (both dangling and unreferenced):

  - `docker system prune` (add -a to remomve unnused images, not just dangling)
