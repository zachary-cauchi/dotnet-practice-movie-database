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
