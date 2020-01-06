FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WorkerService_Demo/WorkerService_Demo.csproj", "WorkerService_Demo/"]
RUN dotnet restore "WorkerService_Demo/WorkerService_Demo.csproj"
COPY . .
WORKDIR "/src/WorkerService_Demo"
RUN dotnet build "WorkerService_Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerService_Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService_Demo.dll"]