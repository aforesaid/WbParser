FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WbParser.Core/WbParser.Core.csproj", "WbParser.Core/"]
COPY ["WbParser.Core/WbParser.Domain.csproj", "WbParser.Domain/"]
COPY ["WbParser.Core/WbParser.Core.csproj", "WbParser.Core/"]
COPY ["WbParser.Core/WbParser.Infrastructure.Data.csproj", "WbParser.Infrastructure.Data/"]
COPY ["WbParser.Core/WbParser.Interface.csproj", "WbParser.Interface/"]

RUN dotnet restore "WbParser.Core/WbParser.Core.csproj"
COPY . .
WORKDIR "/src/WbParser.Core"
RUN dotnet build "WbParser.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WbParser.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WbParser.Core.dll"]
