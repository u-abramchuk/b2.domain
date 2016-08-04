pushd ..\src\b2.Domain.Web
    dotnet publish -c Release
    move bin\Release\netcoreapp1.0\publish ..\..\output
popd