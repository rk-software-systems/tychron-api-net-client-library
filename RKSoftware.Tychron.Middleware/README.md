### About
This package contains Middlewares that can serve as Webhooks for Tychron SMS and MMS Delivery Reports. 
These endpoints can also be used to Receive SMS / MMS / SMS DLR / MMS DLR messages from Tychron API.

### How To Use
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

### Handing Tychron Webhook Requests

To Be able to handle Tychron Webhook requests you need to create a class that implements `IWebhookHandler<T>` interface and register it in DI container.

`T` -  Incoming Webhook Model. It can be one of the following:
- `SmsWebhookModel` - Incoming SMS Webhook Model
- `MmsWebhookModel` - Incoming MMS Webhook Model
- `MmsDlrWebhookModel` - Incoming MMS DLR Webhook Model
- `SMSDLRWebhookModel` - Incoming SMS DLR Webhook Model