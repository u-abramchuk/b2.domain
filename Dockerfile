FROM microsoft/dotnet-nightly:latest

COPY . /app

WORKDIR /app

RUN dotnet restore && dotnet publish -c Release -o output src/b2.Domain.Web

ENV ASPNETCORE_URLS="http://*:5000"

EXPOSE 5000

ENTRYPOINT ["dotnet", "output/b2.Domain.Web.dll"]