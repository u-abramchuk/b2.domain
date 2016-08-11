pushd ..\test\b2.Domain.Tests
    start dotnet watch test
popd
pushd ..\src\b2.Domain.Web
    start dotnet watch run
popd
start docker run -it -p 2113:2113 -p 1113:1113 eventstore/eventstore
start docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management