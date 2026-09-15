# SCOTT / TIGER — 3-Tier Employee Manager

The famous Oracle `EMP` / `DEPT` schema, rebuilt as a modern 3-tier app:

| Tier | Technology | Location |
|---|---|---|
| **1. Presentation** | HTML + CSS + vanilla JS (fetch) | `wwwroot/` |
| **2. Business / API** | ASP.NET Core 8 Web API + service layer | `Controllers/`, `Services/` |
| **3. Data** | Entity Framework Core → SQL Server | `Data/`, `Models/` |

Seeded on first run with the original 4 departments and 14 employees
(KING the PRESIDENT with no manager, SMITH the CLERK under FORD, and the
rest — exactly as they shipped).

---

## Run it in 2 minutes (LocalDB — zero setup)

Requires the **.NET 8 SDK** and (on Windows) **SQL Server LocalDB**, which
ships with Visual Studio.

```bash
dotnet restore
dotnet run
```

Then open the URL it prints (e.g. `https://localhost:7080`).
The database is created and seeded automatically on first launch.

- **App UI:** `/`
- **Swagger / API explorer:** `/swagger`
- **Raw API:** `/api/employees`, `/api/departments`

## Point it at real SQL Server (e.g. Developer edition on your VM)

Edit `appsettings.json` → `ConnectionStrings:ScottConnection`:

```
Configure the database connection using environment variables or a secure secret store. Do not hardcode credentials in source control.
```

Run again — EF Core creates and seeds the schema on that server.
Prefer to create the tables by hand? Run `sql/scott_schema_sqlserver.sql`
in SSMS instead (then remove the `EnsureCreated()` call in `Program.cs`).

## Moving to EF migrations (production path)

`EnsureCreated()` is a demo shortcut. For a real pipeline:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialScott
dotnet ef database update
```

…and replace `db.Database.EnsureCreated();` in `Program.cs` with
`db.Database.Migrate();`.

---

## Drop into Azure Repos

```bash
git init
git add .
git commit -m "SCOTT/TIGER 3-tier app baseline"
git remote add origin https://dev.azure.com/{org}/{project}/_git/{repo}
git push -u origin main
```

This is the app that fuels the migration POC: build it in Azure Pipelines,
deploy to dev/prod, then migrate the repo to GitHub while Boards and
Pipelines keep driving it.
