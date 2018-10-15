# MultiTenantDemo

Web API Solution demonstrates multi-tenancy architecture, using Entity Framework, dependency injection and Repository patterns

![Architecture diagram](https://raw.githubusercontent.com/gopikrishnareddy93/MultiTenantDemo/master/assets/architecture.jpg)

## Working
1. Every request should be accompanied by tenantid header to identify client.
2. Based on tenantid connection string will be modified appropriately.
3. JWT Authroization is enabled. For generating a token client would send appropriate userid and password in the body and tenantid in the header. For additional security tokenid is included in the claims while generating a token and it will be validated on every request.
4. This architecture can be easily modified to support adding new clients without restarting the app.
5. Based on load test results I have found that this architecture has slightly higher response time(~1-2 ms) compared to stand alone app at 100 reqs/sec load. Under normal load both architectures has similar response times.

## Installation

1. Clone repository
2. Run scripts in SQL Scripts directory for creating schema and populating data.
3. Fill up valid database connection string configuration option in `appsettings.json`.
4. Build / Run.
5. Swagger URL: http://localhost:52217/swagger

