# Kustomer Project Repository

This project is an implementation of the Clean Architecture with the CQRS Pattern using .NET 9. 

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/download) or [JetBrains Rider](https://www.jetbrains.com/rider/download/#section=windows)
- [Git](https://git-scm.com/downloads) (to clone the repository)

## Installing the project
**1. Clone the project:**
```
git clone https://github.com/tcorkran/Kustomer.git
```
**2. Restore the packages by running:**
```
# From the solution folder
dotnet restore
```

**3. Run the app:**
```
# From terminal/command prompt
cd Kustomer.API
dotnet run
```

**4. [Kubernetes](/docs/KUBERNETES.md)**


**5. [Scalar OpenAPI/Swagger](/docs/SCALAR.md)**


**6. [Observability](/docs/ASPIRE.md)**


## CICD
[Read more on the CICD pipeline](/docs/CICD.md)

## Testing the project
```
# Tests are split into Unit, Integration and Functional tests
dotnet test
```

## Playing with App Integration
Todo

## Future Roadmap
- Replace controllers with [FastEndpoints](https://github.com/FastEndpoints/FastEndpoints)
- Add more test coverage
- Add CI/CD
- Add validations
- Add more logging
- Add more monitoring
- Add DTOs
