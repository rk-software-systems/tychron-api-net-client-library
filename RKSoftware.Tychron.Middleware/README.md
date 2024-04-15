### About
This package contains Middlewares that can serve as Webhooks for Tychron SMS and MMS Delivery Reports. 
These endpoints can also be used to Receive SMS / MMS / SMS DLR / MMS DLR messages from Tychron API.

### How To Use
- Register services in Startup.cs or Program.cs
```
services.RegisterTychronServices();
```
- Register Middleware in Startup.cs or Program.cs
```
// Register Tychron Middleware for SMS
app.UseTychronMiddleware("/tychron/handler", (appBuilder) =>
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

### Handing Tychron Webhook Requests

To Be able to handle Tychron Webhook requests you need to create a class that implements `IWebhookHandler<T>` interface and register it in DI container.

`T` -  Incoming Webhook Model. It can be one of the following:
- `SmsWebhookModel` - Incoming SMS Webhook Model
- `MmsWebhookModel` - Incoming MMS Webhook Model
- `DlrWebhookModel` - Incoming SMS and MMS DLR Webhook Model

```
services.AddScoped<IWebhookHandler<SmsWebhookModel>, SmsWebhookHandler>();
services.AddScoped<IWebhookHandler<MmsWebhookModel>, MmsWebhookHandler>();
services.AddScoped<IWebhookHandler<DlrWebhookModel>, DlrWebhookHandler>();
```