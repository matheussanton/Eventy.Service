FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./src .
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .

# Install the Entity Framework tool
RUN dotnet tool install --global dotnet-ef --no-cache

# Add the tool to the PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "Eventy.Service.Host.dll"]
