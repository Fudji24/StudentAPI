# StudentApp back-end

Web API for Student application. Made in .Net Web API.

### Technologies
- C#
- .Net Web API
- Entity Framework

### Version

- .Net Framework 4.7.2


## Configuration

In project navigate to Web.Config. Under <configuration> locate:
```
<connectionStrings>
		<add name="ConnectionString" connectionString="Data Source=SERVER_NAME; Integrated Security=true;Initial Catalog=DATABASE_NAME;" providerName="System.Data.SqlClient" />
	</connectionStrings>
```

and change 'SERVER_NAME' with your Server name and 'DATABASE_NAME' with your Database name.
