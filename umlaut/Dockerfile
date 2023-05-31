FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./umlaut.sln ./
COPY ./Umlaut.WebService/Umlaut.WebService.csproj  ./Umlaut.WebService/Umlaut.WebService.csproj
COPY ./Umlaut.HHruAPI/Umlaut.HHruAPI.csproj  ./Umlaut.HHruAPI/Umlaut.HHruAPI.csproj 
COPY ./Umlaut.Database/Umlaut.Database.csproj  ./Umlaut.Database/Umlaut.Database.csproj
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Umlaut.WebService.dll"]