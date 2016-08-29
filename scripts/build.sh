#!/bin/bash

# fix https://github.com/dotnet/cli/issues/3681 
# using https://github.com/dotnet/cli/issues/3681#issuecomment-242912140
apt-get update \
    && apt-get install unzip \
    && mkdir tmp-update \
    && cd tmp-update \
    && wget https://www.nuget.org/api/v2/package/runtime.debian.8-x64.Microsoft.NETCore.Runtime.CoreCLR/1.0.4 \
    && mv 1.0.4 tmp.zip \
    && unzip tmp.zip \
    && cp ./runtimes/debian.8-x64/lib/netstandard1.0/* /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.0 \
    && cp ./runtimes/debian.8-x64/native/* /usr/share/dotnet/shared/Microsoft.NETCore.App/1.0.0 \
    && cd .. \
    && rm -rf tmp-update

dotnet restore --packages packages && dotnet publish -c Release -o output src/b2.Domain.Web