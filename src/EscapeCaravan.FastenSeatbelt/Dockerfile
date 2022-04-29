##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["EscapeCaravan.FastenSeatbelt/EscapeCaravan.FastenSeatbelt.csproj", "EscapeCaravan.FastenSeatbelt/"]
#RUN dotnet restore "EscapeCaravan.FastenSeatbelt/EscapeCaravan.FastenSeatbelt.csproj"
#COPY . .
#WORKDIR "/src/EscapeCaravan.FastenSeatbelt"
#RUN dotnet build "EscapeCaravan.FastenSeatbelt.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "EscapeCaravan.FastenSeatbelt.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "EscapeCaravan.FastenSeatbelt.dll"]



# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY EscapeCaravan.FastenSeatbelt/*.csproj ./EscapeCaravan.FastenSeatbelt/
RUN dotnet restore -r linux-musl-arm

# copy everything else and build app
COPY EscapeCaravan.FastenSeatbelt/. ./EscapeCaravan.FastenSeatbelt/
WORKDIR /src
RUN dotnet publish EscapeCaravan.FastenSeatbelt/EscapeCaravan.FastenSeatbelt.csproj -c release -o /app -r linux-musl-arm --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-arm32v7
WORKDIR /app
COPY --from=build /app ./

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
# ENV \
#     DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#     LC_ALL=en_US.UTF-8 \
#     LANG=en_US.UTF-8
# RUN apk add --no-cache icu-libs

ENTRYPOINT ["dotnet", "EscapeCaravan.FastenSeatbelt.dll"]
#ENTRYPOINT ["./aspnetapp"]