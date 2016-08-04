FROM microsoft/dotnet:latest

COPY ./output /app

WORKDIR /app

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "b2.Domain.Web.dll"]
