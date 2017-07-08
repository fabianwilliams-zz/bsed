# Fabian Doing by Learning Docker Experience
This repo is all about me learning Docker by doing it from the ground up. I wanted this to be here to 
- Help me remember what I did to start from the proverbial Zero to Hero in short order
- Help out folks out there looking to learn Docker thats coming from a Microsoft Developer background
More background info: [BLOG: Docker ASPNet Core WebAPI with MSSQL Linux Backend 6 part Series](http://www.fabiangwilliams.com/2017/06/05/building-a-docker-backend-for-xamarin-mobile-development-series/)

## Approaches you can take here to get to a working solution

We will work from the least effort to the most effort, in doing that we will still get to the same solution but one way will just get you a working solution up and running in a few minutes, the second approach will get you same please, but you will have a copy of the solution as well in YOUR own custom image. All approches outlined below assumes you have Docker already installed and set up ready to go, however if that is not the case, the first 2 part of the Blog series above will show you how to get to that point.

### Use my custom image in Docker Hub to spin up the solution
Do the following:
- Create a Directory where you want your solution to live
- In that directory create a file called "docker-compose.yml"
- Copy the folloing into the file:

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

### Clone my Repo here and build your own solution

This solution is by far the simpliest and just requrires you to do the following 
Run the following:

```
	git clone https://github.com/fabianwilliams/bsed.git
	cd bsed
	docker-compose build
	docker-compose up -d
```
You can now access the Dockerized app at: [http://localhost:8000](http://localhost:8000)



