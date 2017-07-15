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
## First test
```csharp
[Fact]
public void First_Term_Is_Zero()
{
    var sut = new FibonacciCalculator();
    var result = sut.Calculate(1);
    Assert.Equal(new[] { 0 }, result.ToArray());
}
```

## Next tests
```csharp
[Fact]
public void Second_Term_Is_One()
{
    var sut = new FibonacciCalculator();
    var result = sut.Calculate(2);
    Assert.Equal(new[] { 0, 1 }, result.ToArray());
}

[Fact]
public void Third_Term_Is_One()
{
    var sut = new FibonacciCalculator();
    var result = sut.Calculate(3);
    Assert.Equal(new[] { 0, 1, 1 }, result.ToArray());
}
```
## Refactor to parameterised test
```csharp
[Theory]
[InlineData(3, 1)]
public void Later_Terms_Are_The_Sum_Of_The_Two_Previous_Terms(int term, int expected)
{
    var sut = new FibonacciCalculator();
    var result = sut.Calculate(term).ToArray();
    Assert.Equal(expected, result[term - 1]);
}
```