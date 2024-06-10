# STW Submission API

The Submission API is a .NET Core Web API that will receive, validate and place UNCEFACT payloads on an Azure Service
Bus Queue.

## Running the Application

You can either run the application directly on your local machine or via Docker.

### Running via Docker

#### Prerequisites

If not already installed, you will need [Docker Desktop](https://www.docker.com/products/docker-desktop).

#### Steps

1. Open terminal and `cd` into the root of the repository
2. Run `docker-compose build`
3. Run `docker-compose up`

Following the completion of the steps above, the application should now be running on port `5244`.

### Running on Local Machine

#### Prerequisites

If not already installed, you will need:

- .NET 8 Runtime and SDK - [Microsofts .NET 8 Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

The environment variables can be set by creating `appsettings.Development.json`
in `src/STW.SubmissionApi.Presentation/`:

```json
{
  "ServiceBusConnectionString": "<service-bus-connection-string>",
  "ServiceBusQueueName": "<queue-name>"
}
```

#### Steps

1. Open terminal and `cd` into the `src/STW.SubmissionApi.Presentation` directory
2. Run `dotnet run`

Following the completion of the steps above, the application should now be running on the port specified in the console
output.

## Licence

THIS INFORMATION IS LICENSED UNDER THE CONDITIONS OF THE OPEN GOVERNMENT LICENCE found at:

<http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3>

The following attribution statement MUST be cited in your products and applications when using this information.

> Contains public sector information licensed under the Open Government licence v3

### About the licence

The Open Government Licence (OGL) was developed by the Controller of Her Majesty's Stationery Office (HMSO) to enable
information providers in the public sector to license the use and re-use of their information under a common open
licence.

It is designed to encourage use and re-use of information freely and flexibly, with only a few conditions.