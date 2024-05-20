
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0.414-alpine3.18 AS build
WORKDIR /src


COPY ["Payments.Api/Payments.Api.csproj", "src/Payments.Api/"]
COPY ["Payments.Domain/Payments.Domain.csproj", "src/Payments.Domain/"]
COPY ["Payments.Infrastructure/Payments.Infrastructure.csproj", "src/Payments.Infrastructure/"]
COPY ["Payments.Tests/Unity.Tests.csproj", "src/Payments.Tests/"]

RUN dotnet restore "src/Payments.Api/Payments.Api.csproj"

COPY . .
WORKDIR "/src/Payments.Api"


RUN dotnet build "Payments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payments.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.Api.dll"]
