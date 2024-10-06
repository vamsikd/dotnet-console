FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /source

COPY Configuration.csproj .
RUN dotnet restore
COPY . .

RUN dotnet publish -c release -o /app --no-restore  --no-self-contained

FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS runtime

WORKDIR /app
COPY --from=build /app .

# Use a non-root user to run the app for better security
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser

ENTRYPOINT ["dotnet", "Configuration.dll"]