FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sandbox/Sandbox.fsproj", "Sandbox/"]
RUN dotnet restore "Sandbox/Sandbox.fsproj"
COPY . .
WORKDIR "/src/Sandbox"
RUN dotnet build "Sandbox.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sandbox.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sandbox.dll"]
