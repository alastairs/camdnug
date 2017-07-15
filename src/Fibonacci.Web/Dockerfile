FROM microsoft/aspnetcore:1.1
WORKDIR /app

COPY bin/Release/netcoreapp1.1/publish/ ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Fibonacci.Web.dll"]
