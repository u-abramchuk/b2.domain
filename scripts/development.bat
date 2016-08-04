pushd ..\test\b2.Domain.Tests
    start dotnet watch test
popd
pushd ..\src\b2.Domain.Web
    start dotnet watch run
popd