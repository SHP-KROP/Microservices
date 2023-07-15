rmdir /S /Q Migrations

dotnet ef migrations add InitialIsentityServerMigration -c ConfigurationDbContext -o Migrations/ConfigurationDb
dotnet ef migrations add InitialIsentityServerMigration -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
dotnet ef migrations add InitialIsentityServerMigration -c ApplicationDbContext -o Migrations/ApplicationDb

dotnet ef database update -c ConfigurationDbContext
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ApplicationDbContext

dotnet run bin/Debug/net6.0/AuthServer /seed
