# New project

```powershell
dotnet new sln -o .
dotnet new mvc -o ./src/Fibonacci.Web
dotnet sln add .\src\Fibonacci.Web\Fibonacci.Web.csproj
dotnet new xunit -o ./test/Fibonacci.Web.Test
dotnet sln add .\test\Fibonacci.Web.Test\Fibonacci.Web.Test.csproj
pushd .\test\Fibonacci.Web.Test
dotnet add reference ..\..\src\Fibonacci.Web\Fibonacci.Web.csproj
popd
dotnet restore
dotnet build
dotnet test .\test\Fibonacci.Web.Test\Fibonacci.Web.Test.csproj
edit .
```

# Testing Fibonacci

```csharp
[Fact]
public void First_Term_Is_Zero()
{
    var sut = new FibonacciCalculator();
    var result = sut.Calculate(1);
    Assert.Equal(new[] { 0 }, result.ToArray());
}
```
