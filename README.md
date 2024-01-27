# Tychron API Wrapper packages for .NET

| Package  |  Current version and Downloads
|---|---|
|  RKSoftware.Tychron.APIClient  | [![NuGet Badge](https://buildstats.info/nuget/RKSoftware.Tychron.APIClient)](https://www.nuget.org/packages/RKSoftware.Tychron.APIClient/)
|  RKSoftware.Tychron.Middleware |  [![NuGet Badge](https://buildstats.info/nuget/RKSoftware.Tychron.Middleware)](https://www.nuget.org/packages/RKSoftware.Tychron.Middleware/)

## RKSoftware.Tychron.APIClient

### About
This package is a simple wrapper around the [Tychron API](https://docs.tychron.info/). It allows you to query Tychron API easily from within your .NET code. You can send SMS, SMS DLR, MMS, MMS DLR using Tychron API.

### How To Use
There are to ways to use Tychron Clients.

- Create a new instance of API Clients directly in code and use them.
```
// create Http Client (it is better to obtain if rom HttpClientFactory)
var httpClient = new HttpClient();
// Set Tychron API related data (Authorization header and base url)
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerKey);
httpClient.BaseAddress = baseUrl;

// Create Tychron API Clients
var smsDLRclient = new TychronSMSDLRClient(httpClient);
var mmsDLRclient = new TychronMMSDLRClient(httpClient);
var smsClient = new TychronSMSClient(httpClient);

// Make requests
```
- ASP.NET Core Dependency Injection
```
// Register Tychron API Clients in Startup.cs
services.RegisterTychronClients(bearerKey, baseUrl);

// Inject Tychron API Clients in your controllers or services
public class MyController : Controller
{
    private readonly ITychronSMSDLRClient _smsDLRclient;
    private readonly ITychronMMSDLRClient _mmsDLRclient;
    private readonly ITychronSMSClient _smsClient;

    public MyController(ITychronSMSDLRClient smsDLRclient, ITychronMMSDLRClient mmsDLRclient, ITychronSMSClient smsClient)
    {
        _smsDLRclient = smsDLRclient;
        _mmsDLRclient = mmsDLRclient;
        _smsClient = smsClient;
    }

    // Use Tychron API Clients
}
```

## RKSoftware.Tychron.Middleware

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