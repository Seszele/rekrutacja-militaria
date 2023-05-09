FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY TestConsole/TestConsole.csproj TestConsole/
COPY MailLibrary/MailLibrary.csproj MailLibrary/
RUN dotnet restore "TestConsole/TestConsole.csproj"
COPY . .
RUN dotnet build "TestConsole/TestConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestConsole/TestConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for.sh /app/wait-for.sh
RUN chmod +x /app/wait-for.sh

# Install netcat
RUN apt-get update && apt-get install -y netcat

ENTRYPOINT ["/app/wait-for.sh", "rabbitmq", "dotnet", "TestConsole.dll"]
