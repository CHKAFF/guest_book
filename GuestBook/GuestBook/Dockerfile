FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY GuestBook.csproj ./
RUN dotnet restore GuestBook.csproj    

COPY . .
RUN dotnet build GuestBook.csproj -c Release -o out
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/Properties/launchSettings.json ./Properties/launchSettings.json
COPY --from=build-env /app/.version ./.version

# Это нужно чтобы Ydx.SDK однялся на линукск
RUN apt update
RUN apt install -y libc-dev

ENTRYPOINT ["dotnet", "GuestBook.dll"]