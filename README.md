# Fabian Doing by Learning Docker Experience
This repo is all about me learning Docker by doing it from the ground up. I wanted this to be here to 
- Help me remember what I did to start from the proverbial Zero to Hero in short order
- Help out folks out there looking to learn Docker thats coming from a Microsoft Developer background
More background info: [BLOG: Docker ASPNet Core WebAPI with MSSQL Linux Backend 6 part Series](http://www.fabiangwilliams.com/2017/06/05/building-a-docker-backend-for-xamarin-mobile-development-series/)

## Approaches you can take here to get to a working solution

We will work from the least effort to the most effort, in doing that we will still get to the same solution but one way will just get you a working solution up and running in a few minutes, the second approach will get you same place, but you will have a copy of the solution as well in YOUR own custom image. All approches outlined below assumes you have Docker already installed and set up ready to go, however if that is not the case, the first 2 part of the Blog series above will show you how to get to that point.

### Use my custom image in Docker Hub to spin up the solution
Do the following:
- Create a Directory where you want your solution to live
- Go into that directory create a file called "docker-compose.yml"
- Copy the following into the file:

```
    version: "3"
    services:
        web:
            image: "fabianwilliams/fabsevalswebapi"
            ports:
                - "8000:80"
            depends_on:
                - db
        db:
            image: "microsoft/mssql-server-linux"
            ports:
                - "1433:1433"
            environment:
                SA_PASSWORD: "P@ssword1!"
                ACCEPT_EULA: "Y"
```

then run the following

```
	docker-compose up -d
```
You can now access the Dockerized app at: [http://localhost:8000](http://localhost:8000)

### Clone my Repo here and build your own solution

This solution will make a clone of what I have here in Github, and will result in you having your own solution. 
Run the following:

```
	git clone https://github.com/fabianwilliams/bsed.git
	cd bsed
	docker-compose build
	docker-compose up -d
```
You can now access the Dockerized app at: [http://localhost:8000](http://localhost:8000)

One thing to note about this solution that is quite important is the YML file that you see above has one line that is swapped out. the line that has the image in there would be gone and in its place would be 

![](https://i0.wp.com/www.fabiangwilliams.com/wp-content/uploads/2017/07/image_thumb-19.png)

this one uses whats inside the DockerFile below. which takes the base image of aspnetcore and layers it with this Web.API solution. The docker file looks like:

![](https://i2.wp.com/www.fabiangwilliams.com/wp-content/uploads/2017/07/image-17.png)

## Running my Fabian Evals Docker Solution using Docker Swarm

I've now added the capability to put my solution in Swarm Mode!!! You will need to modify my Docker Compose File a bit to add the DEPLOY section.  In this case we are adding 2 Replicas ONLY for the WEB service which means it will run as in my case on 2 Nodes since i have more than 1 Ubuntu VM stood up. But you can still get multiple container replicas even if you have 1 Node, it just would mean you would have multiple replicas on 1 node.

```
version: "3"
services:
    web:
        image: "fabianwilliams/fabsevalswebapi"
        build: .
        deploy:
            replicas: 2
            restart_policy:
                condition: on-failure
        ports:
            - "8000:80"
        depends_on:
            - db
    db:
        image: "microsoft/mssql-server-linux"
        ports:
            - "1433:1433"
        environment:
            SA_PASSWORD: "P@ssword1!"
            ACCEPT_EULA: "Y"
```

Make sure you are running in swarm mode (`docker swarm init`). You can then run the following to deploy the containers to Docker Swarm with:

```
	docker stack deploy -c docker-compose.yml FabsEvals
```

Thank you all. I hope you have as much fun as I did makeing this. 

