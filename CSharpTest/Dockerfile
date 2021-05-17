FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CSharpTest/CSharpTest.fsproj", "CSharpTest/"]
RUN dotnet restore "CSharpTest/CSharpTest.fsproj"
COPY . .
WORKDIR "/src/CSharpTest"
RUN dotnet build "CSharpTest.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpTest.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpTest.dll"]
