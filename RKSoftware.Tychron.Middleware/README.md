## About
This package contains Middlewares that can serve as Webhooks for Tychron SMS and MMS Delivery Reports. 
These endpoints can also be used to Receive SMS / MMS / SMS DLR / MMS DLR messages from Tychron API.

## How To Use
### Authentication Middlewares
According to Tychron API Documentation, you can use Basic Authentication or Bearer Authentication.
There are to ways to use Tychron Clients.

- Register Middleware in Startup.cs or Program.cs
```
// Register Tychron Middleware for SMS
app.UseTychronMiddleware<SmsWebhookModel>("/tychron/sms", (appBuilder) =>
{
    // Use Basic Auth for Tychron requests
    appBuilder.UseTychronBasicAuth("username", "password");

    // Use Bearer Auth for Tychron requests
    // appBuilder.UseTychronBearerAuth("token");
});

// Register Tychron Middleware for MMS
app.UseTychronMiddleware<MmsWebhookModel>("/tychron/mms", (appBuilder) =>
{
    // Use Basic Auth for Tychron requests
    appBuilder.UseTychronBasicAuth("username", "password");

    // Use Bearer Auth for Tychron requests
    // appBuilder.UseTychronBearerAuth("token");
});

// Register Tychron Middleware for MMS DLR
app.UseTychronMiddleware<MmsDlrWebhookModel>("/tychron/mms-dlr", (appBuilder) =>
{
    // Use Basic Auth for Tychron requests
    appBuilder.UseTychronBasicAuth("username", "password");

    // Use Bearer Auth for Tychron requests
    // appBuilder.UseTychronBearerAuth("token");
});

// Register Tychron Middleware for SMS DLR
app.UseTychronMiddleware<SMSDLRWebhookModel>("/tychron/sms-dlr", (appBuilder) =>
{
    // Use Basic Auth for Tychron requests
    appBuilder.UseTychronBasicAuth("username", "password");

    // Use Bearer Auth for Tychron requests
    // appBuilder.UseTychronBearerAuth("token");
});
```

Tychron Webhook supports Basic and Bearer Authentication. You can use any of the two methods to authenticate Tychron requests.
```
// Use Basic Auth for Tychron requests
appBuilder.UseTychronBasicAuth("username", "password");

// Use Bearer Auth for Tychron requests
// appBuilder.UseTychronBearerAuth("token");
```

Register Tychron Authentication Middleware only in AuthConfigurator parameter of UseTychronMiddleware method to make auth affect only Tychron requests.