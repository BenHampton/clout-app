ROLE ?= Scheduler
build:
	dotnet build
clean:
	dotnet clean
restore:
	dotnet restore
watch:
	dotnet watch --project src/clout-api.csproj run
start:
	dotnet run --project src/clout-api.csproj
test:
	dotnet test
add-migration:
	dotnet ef migrations add "$(MIGRATION)" --project src/clout-api.csproj
remove-migration:
	dotnet ef migrations remove --project src/clout-api.csproj
update-database:
	dotnet ef database update --project src/clout-api.csproj
init-secrets:
	dotnet user-secrets init --project src/clout-api.csproj
	dotnet user-secrets set --project src/clout-api.csproj ConnectionStrings:postgres "Host=localhost;Database=postgres;Username=postgres;Password=postgres"
user-jwt:
	dotnet user-jwts create --project src --claim role="$(ROLE)"
docker-jwt:
	dotnet user-jwts create --project src --claim role="$(ROLE)" --audience "http://localhost:8080"
