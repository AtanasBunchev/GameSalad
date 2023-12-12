# Overview

A simple ASP.NET Core application.
Nothing really interesting here.

The purpose of this project is to let me experience Test Driven Development.
Considering my lack of experience on the topic i concluded that a small project
will be best suited for this purpose.


# Building

## Using Docker Compose

```bash
# Run the application
docker compose up -d
# Stop the application
docker compose down
```

The `compose.yaml` file specifies the services that Docker has to bring up for the app to work.
These are the application itself and Microsoft's SQL server container.
Feel free to modify it to suit your needs or expectations.

**Note:** Currently the data stored in the SQL server is contained only in the container and is not persistent. If you want to have persistent data while running the app using Docker you need to modify `compose.yaml` yourself.
Check Microsoft's [learning resource on their SQL server container](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-configure?view=sql-server-ver16&pivots=cs1-bash) and Docker's documentation on [volume configuration in compose files](https://docs.docker.com/compose/compose-file/compose-file-v3/#volume-configuration-reference).

**Note:** Currently using the Docker compose approach to run the app will run a developer build using the dotnet watchdog to reload any files modified while developing.


## Running without Docker Compose

You have to modify the [connection string](https://www.connectionstrings.com/)
[in this file](src/Repositories/UsersDbContext.cs) to suit your database setup.

Once you've set up and started a database server and configured the application accordingly
you can start the application:

```bash
cd src/
dotnet run
```

The application will create it's database by applying the available migrations at runtime.


## Running the tests

Currently the tests can only be run without using a container.
_You can modify the_ `Dockerfile` _to run them every time the container is started._

```bash
cd tests/
dotnet test
```

Tests that interact with the database have a
[separate connection string](tests/Repositories/TestUsersDbContext.cs)
and use In-Memory SQLite database instances.

