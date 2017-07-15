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
## Add further test cases
```csharp
[InlineData(4, 2)]
[InlineData(5, 3)]
[InlineData(6, 5)]
[InlineData(7, 8)]
[InlineData(8, 13)]
[InlineData(9, 21)]
```
# Dockerise
## Dockerfile
```dockerfile
FROM microsoft/aspnetcore:1.1
WORKDIR /app

COPY bin/Release/netcoreapp1.1/publish/ ./

EXPOSE 80

ENTRYPOINT ["dotnet", "Fibonacci.Web.dll"]
```
## Building
```powershell
cd src/Fibonacci.Web
dotnet publish -c Release
docker build -t alastairs/fibonacci:latest .
```
## Running
```powershell
docker run -p 80:80 --name fibonacci alastairs/fibonacci:latest
docker rm -f fibonnaci
# Turn up log level
docker run -p 80:80 -e Logging__LogLevel__Default=Information --name fibonacci alastairs/fibonacci:latest
```
# Kubernetes
## Creating a cluster on Azure Container Service
```powershell
param(
    [Parameter(Mandatory=$true)]
    [string]$SshKeyFile,

    [Parameter(Mandatory=$true)]
    [string]$Location = "northeurope",

    [string]$ResourceGroup = "k8s-$Location",
    [string]$ClusterName = "k8s-$Location",
    [string]$DnsPrefix = "fibonacci",
    [int]$MasterCount = 1,
    [int]$AgentCount = 3,
    [switch]$Force
)

if ($Force) {
    az group delete --name=$ResourceGroup --yes
}

az group create --name=$ResourceGroup --location=$Location

az acs create --orchestrator-type=kubernetes --resource-group=$ResourceGroup `
    --name=$ClusterName --dns-prefix=$DnsPrefix --ssh-key-value="$SshKeyFile.pub" `
    --master-count $MasterCount --agent-count $AgentCount

az acs kubernetes get-credentials --resource-group=$ResourceGroup --name=$ClusterName --ssh-key-file=$SshKeyFile
```

```bash
cd /mnt/c/Users/AlastairSmith/.ssh
ssh-keygen
```

## Deploying the image to Kubernetes
```yaml
---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  creationTimestamp: null
  name: web
spec:
  replicas: 1
  revisionHistoryLimit: 2
  strategy: {}
  template:
    metadata:
      creationTimestamp: null
      labels:
        service: web
    spec:
      containers:
      - image: alastairs/fibonacci:latest
        name: web
        ports:
        - containerPort: 80
        resources: {}
      restartPolicy: Always
status: {}
---
apiVersion: v1
kind: Service
metadata:
  creationTimestamp: null
  labels:
    service: web
  name: web
spec:
  ports:
  - name: "http"
    port: 80
    targetPort: 80
  selector:
    service: web
  type: LoadBalancer
---
```