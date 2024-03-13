# .NET Core MediatR API Authentication Example

This is a sample repository to demonstrate the capabilities of [MediatR](https://github.com/jbogard/MediatR) to append user context onto API requests to be used in business logic. This repository also shows how [FluentValidation](https://docs.fluentvalidation.net/en/latest/) can be used in conjunction with MediatR to abstract away validating the requests.

There are a couple of unit tests and API tests in this project. For API testing, this project is using [Hurl](https://hurl.dev/) to make API requests and assert on responses.

## Getting Started

There are two ways to run this project: using bare metal or using Docker. Here are the installation steps for both.

### Installation: Bare Metal
Tools:
- .NET 8.0+
- Hurl 4.0+

#### Building .NET Solution / Running Unit Tests

Once .NET is installed, open the `src/api/Dappa/Dappa.sln` solution in Visual Studio or JetBrains Rider. Your IDE should automatically install all dependencies needed to run the project, but you can always run NuGet Package Restore to install. From the solution, you can run the `http` or `https` configuration to start the API. You can run the `Dappa.Core.Tests` solution with `Run Unit Tests` to run all the unit tests.

If you prefer the CLI route, the following instructions will work:

```bash
$ cd src/api/Dappa
$ dotnet build # build solution
$ dotnet test # test solution
$ dotnet run Dappa.sln --project Dappa.Api # run api
```

#### Running API Tests

To run the Hurl tests, first ensure the API is running using the steps listed above. Once this is confirmed to be running, you can run the Hurl test suite by running the following from the project root:

```bash
$ hurl --variables-file ./tests/api/vars.env --test tests/api/*.hurl
```

### Installation: Docker
Tools:
- Docker

#### Running Docker Instances

To run the Docker containers, you can run the following commands from the root directory:

```bash
$ docker-compose -f ./docker-compose.yaml -f docker/docker-compose.api.yaml up --build -d  # start the API as a detached process
$ docker-compose -f ./docker-compose.yaml -f docker/docker-compose.test.yaml up --build     # run the Hurl test suite
```


