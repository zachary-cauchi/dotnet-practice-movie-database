# Notes

## Project traversal

Controller (API Request) -> Grain Client (Grain get/set) -> Grain Factory (Creating Grain Get/Set) -> Grain (Create or get underlying model state)

## Required movie models:
- [x] Movie model
- [x] Grain interfaces.
- [x] Create movie grain.
- [x] Create movie grain factory.
- [x] Create movie grain client.

## Side notes on grains

* Each grain is like a wrapper around an individual model instance; it's responsible for the state of that model and any operations to be done on it.
  Like this, grains are able to persist their state and subsequently the model as well as keep business logic away from the model itself.
* Data validation needs to be added to each field.

## Grain persistence.

Now with basic Movie model support, we need to add persistence to allow loading of grains at startup instead of using non-persistent memory storage.

Likely candidate is an SQL database with ADO.NET as a db provider for Orleans.

Dug into code and best way of doing it might be to modify the appSettings json or similar program. App settings are loaded from Program.cs.
Silo persistence is configured in SiloBuilderExtensions.cs.

### Storage provider considerations

First attempted setting up a local hosted mssql database, but decided against it due to the initial setup required.

Moved to Azure Table setup, managed to establish connection, now need to configure persistence of the movie grains.

### Legacy grain state persistence

According to the [Grain Persistence](https://dotnet.github.io/orleans/docs/grains/grain_persistence/index.html#recommendations) documentation, persistence defined through `Grain<T>` is considered _legacy_.
Switched over to using the newer method of persistence by defining `private readonly IPersistentState<Movie> _movie;` as the grains movie state.

## Grain persistence successful!

State-writing is now working as intended; while grain state is stored in-memory, calling `await _state.WriteStateAsync()` causes it to be persisted to all persistence providers (like Azure Tables).
Data is then pulled from the persistence providers on startup and once again stored in-memory.
Connection strings are also stored in user-secrets for security.

To summarise:

non-persistent primary store   persistent secondary store
In-memory                   -> Azure Table Store

## Limitation of Azure Tables

Azure Tables has a limit to the amount of data it can store in a single row (1MB of binary data).
This is unlikely to be an issue at this time since the amount of data per-grain is small (only text-based metadata with no binary data).

## Fixing genre param for movie creation

Currently, the genre is expected to be a comma-separated list. This needs to be changed to an array/list.
Fixed this by changing the parameters list to expect a single `[FromBody] Movie movie`. All the parameters in the movie are consumed, except for the id.

## Added bulk-add movies endpoint

Main purpose is to help seed the grain storage with movie data. Works by fanning out multiple grain-set requests and awaits them all before returning.

## Enabling Transactional behaviour for azure tables

To enable it, the maximum throughput of the account needs to be increased to at least 1600RU/s.
While it may not be a problem in most scenarios, it does exceed the free tier amount.

## Added OrleansDashboard

Added OrleansDashboard for metrics on the MovieSilo. For development only and should be removed for production.

## Stuck with Movie searching

Realised I hit a wall with how I stored the movies as grains; because grains can't be searched or filtered, I cannot fulfil the api requirements using only grains.
This will require storing the movies in a separate storage location (such as another AzureTable) where the data is stored raw and can be queried.
Movies searched for will then be cached in grains
New grains may be needed for the following:
- Top5RatedMovies - Store the ids of the top 5 rated movies in descending order.
- GenreFilterMovies - Store the ids of all (or as many as possible) movies which match that genre.

## Added MoviesService

Added Service layer for handling operations on movies. This should prepare the server for both grains and table management.
