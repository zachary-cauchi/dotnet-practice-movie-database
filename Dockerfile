# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Movies.Contracts/*.csproj ./Movies.Contracts/
COPY Movies.Core/*.csproj ./Movies.Core/
COPY Movies.GrainClients/*.csproj ./Movies.GrainClients/
COPY Movies.Grains/*.csproj ./Movies.Grains/
COPY Movies.Server/*.csproj ./Movies.Server/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /source/
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]