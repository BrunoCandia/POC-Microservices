### Build the Dockerfile

To build image from it, navigate to folder which contains the solution file of your application(D:\Shared\Proyectos\Endava\POC-EntrevistApp\Users.API)
and execute following:

```
docker build --rm -f "Users.API\Dockerfile" .
```

The option --rm remove intermediate containers after a successful build

### Create and start one or more containers for each dependency with a single command

To start the containers, navigate to folder which contains the solution file of your application(D:\Shared\Proyectos\Endava\POC-EntrevistApp\Users.API)
and execute following:

```
docker-compose up
```

If a change is done in the code

```
docker-compose up --build
```

### To use swagger the host is mapped to the port 56198

So navigate to http://localhost:56198/swagger/index.html to test the api

### Sample of Unit of Work

For reference see https://github.com/brunohbrito/MongoDB-RepositoryUoWPatterns
