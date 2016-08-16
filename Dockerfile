FROM microsoft/dotnet:latest

ENV ASPNETCORE_URLS="http://*:5000"

WORKDIR /app

EXPOSE 5000