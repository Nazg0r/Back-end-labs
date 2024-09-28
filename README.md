# How to run the project on the local machine:

To use the program, you need to clone this repository to your local machine:
```
git clone https://github.com/Nazg0r/Back-end-labs.git
```

To compile and run program, you need to have the latest version of dotnet core, which can be downloaded at the following [link](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

Once you have the latest version of dotnet, navigate to the root folder of the project in the terminal and run the following command:
```
dotnet run --project ./API
```
This command will automatically install all dependencies and build the project, after which you can access the healthcheck page by visiting the following links:

http://localhost:5000/healthcheck - by using `http` protocol

https://localhost:5001/healthcheck - by using `https` protocol

## How to run the project in docker

First, you need to launch Docker Desktop. If you don’t have it installed, you can download it from the [link](https://www.docker.com/products/docker-desktop/).

Open the terminal in the project root directory and build the docker image using command:
```
docker build -t <image-name>:<tag> .
```

Now, you can run the container as follows: 
```
docker run -p 5000:8080 --rm <image-name>:<tag>
```

As a result, you will be able to visit the project`s healthcheck page by navigating to http://localhost:5000/healthcheck.

Another way to run the project in Docker is by using the docker-compose comamands.

To run the container use: 
```
docker-compose up
```

And to stop and remove it use:
```
docker-compose down
```

## Go to deploy

You can visit the deployed project's healthcheck page by following the [link](https://back-end-labs-14cq.onrender.com/healthcheck)
