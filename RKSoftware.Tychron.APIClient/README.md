## About
This package is a simple wrapper around the [Tychron API](https://docs.tychron.info/). It allows you to query Tychron API easily from within your .NET code.

## How To Use
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