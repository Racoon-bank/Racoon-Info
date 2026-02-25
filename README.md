# Racoon-Info
Here lies the user logic of the Racoon Bank


## How to run

If you want to connect to db - create a SQLServer db and adjust following credentional in [appsettings.json](api/appsettings.json) file

```
"ConnectionStrings": {
    "SonyaConnection": "Data Source={Server};Initial Catalog={database name};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
```
Server name can be found be expecting **Properties** of your database.
Then ran migrations
```
cd api
dotnet ef migrations add Init
dotnet ef database update
```

To run (in api folder):
```
dotnet watch run
```

After that swagger should appear

### If you just want to see endpoints and DTOs
1. Find [swagger.json](swagger.json) file
2. Copy and paste its contents [here](https://forge.etsi.org/swagger/editor/) - you should see a swagger 😄
