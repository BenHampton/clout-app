Swagger:
http://localhost:5167/swagger/index.html

ROLE ?= Scheduler
build:
dotnet build
clean:
dotnet clean
restore:
dotnet restore
watch:
dotnet watch --project src/ess-ngs-api.csproj run
start:
dotnet run --project src/ess-ngs-api.csproj
test:
dotnet test
add-migration:
dotnet ef migrations add "$(MIGRATION)" --project src/ess-ngs-api.csproj
remove-migration:
dotnet ef migrations remove --project src/ess-ngs-api.csproj
update-database:
dotnet ef database update --project src/ess-ngs-api.csproj
init-secrets:
dotnet user-secrets init --project src/ess-ngs-api.csproj
dotnet user-secrets set --project src/ess-ngs-api.csproj ConnectionStrings:postgres "Host=localhost;Database=postgres;Username=postgres;Password=postgres"
user-jwt:
dotnet user-jwts create --project src --claim role="$(ROLE)"
docker-jwt:
dotnet user-jwts create --project src --claim role="$(ROLE)" --audience "http://localhost:8080"