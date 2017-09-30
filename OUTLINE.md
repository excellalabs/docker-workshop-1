# Docker for Devleopment Workshop: Outline

Follow this workshop (can be self-guided), to learn the topics below, https://github.com/excellalabs/docker-workshop-1

PART I

* What is Docker?
* Why use it for development? Deployment? 
* Pre-LAB: Everyone have Docker installed? 
* LAB: Running a container in Docker
* LAB: Creating a container for your app 
  * Create a Dockerfile for the container, for your app
  * Build and run the container
  * Develop

PART II

* Managing data in containers
  LAB: Managing data with volumes
* Networking
  * LAB: Communication among containers
* Run a database container to use
  * Talk to your database container from your app container via Docker networks
* Compose [I have slides that cover this]
  * LAB: Creating a Compose file for spinning up your whole app 
  * Create a Compose file to manage multiple containers, and container startup settings / environment variables
    * Add the database container
    * Create a hosted volume for the database
    * Run the app

PART III

 * Deploying
 * Clusters
 * Schedulers & Orchestrators
   * Swarm
   * Kubernetes
   * Mesos, etc
 * Logging & Monitoring
 * Docker Enterprise