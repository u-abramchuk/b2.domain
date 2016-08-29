FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

ENV ASPNETCORE_URLS="http://*:5000"

EXPOSE 5000

ENTRYPOINT ["dotnet", "output/b2.Domain.Web.dll"]