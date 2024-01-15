namespace RK.Tychron.APIClient;

internal record TychronAuthorizationToken()
{
    public string bearerKey { get; init; } = $"Bearer Rb2VRMSsm0VxHOIcniw6hCJhUqRExQk2";
    public Uri baseUrl { get; init; } = new("https://sms.tychron.online");
}