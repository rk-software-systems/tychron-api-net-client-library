## About
This package is a simple wrapper around the [Tychron API](https://docs.tychron.info/). It allows you to query Tychron API easily from within your .NET code.

## How To Use
There are to ways to use Tychron Clients.

- Create a new instance of API Clients directly in code and use them.
```
// create Http Client (it is better to obtain it from HttpClientFactory)
var httpClient = new HttpClient();

// Set Tychron API related data (Authorization header and base url)
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerKey);
httpClient.BaseAddress = baseSmsUrl;
// for MMS
// httpClient.BaseAddress = baseMmsUrl

// Create Tychron API Clients
var smsClient = new TychronSmsClient(httpClient); 
// for MMS
// var mmsClient = new TychronMmsClient(httpClient);

// Make requests
```
- ASP.NET Core Dependency Injection
```
// Register Tychron API Clients in Startup.cs
services.RegisterTychronClient<TychronSmsClient>(baseSmsUrl, bearerKey);
services.RegisterTychronClient<TychronMmsClient>(baseMmsUrl, bearerKey)

// Inject Tychron API Clients in your controllers or services
public class MyController : Controller
{    
    private readonly TychronMmsClient _mmsClient;
    private readonly TychronSmsClient _smsClient;

    public MyController(TychronMmsClient mmsClient, TychronSmsClient smsClient)
    {
        _mmsClient = mmsClient;
        _smsClient = smsClient;
    }

    // Use Tychron API Clients
}
```