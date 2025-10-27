# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj first (faster layer caching)
COPY Todo.Api/Todo.Api.csproj Todo.Api/
RUN dotnet restore Todo.Api/Todo.Api.csproj

# copy the rest and publish
COPY Todo.Api/ ./Todo.Api/
RUN dotnet publish Todo.Api -c Release -o /app

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet","Todo.Api.dll"]