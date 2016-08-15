FROM microsoft/dotnet:latest

ENV ASPNETCORE_URLS="http://*:5000"

COPY ./output /app

WORKDIR /app

EXPOSE 5000

ENTRYPOINT ["dotnet", "b2.Domain.Web.dll"]