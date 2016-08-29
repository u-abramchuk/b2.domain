FROM microsoft/dotnet-nightly:latest

COPY . /app

WORKDIR /app

RUN apt-get update \
    && apt-get install unzip \
    && mkdir tmp-update \
    && cd tmp-update \
    && wget https://www.nuget.org/api/v2/package/runtime.ubuntu.14.04-x64.Microsoft.NETCore.Runtime.CoreCLR/1.0.4 \
    && mv 1.0.4 tmp.zip \
    && unzip tmp.zip \
    && cp ./runtimes/ubuntu.14.04-x64/lib/netstandard1.0/* /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.0 \
    && cp ./runtimes/ubuntu.14.04-x64/native/* /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.0 \
    && cd .. \
    && rm -rf tmp-update

RUN dotnet restore && dotnet publish -c Release -o output src/b2.Domain.Web

ENV ASPNETCORE_URLS="http://*:5000"

EXPOSE 5000

ENTRYPOINT ["dotnet", "output/b2.Domain.Web.dll"]