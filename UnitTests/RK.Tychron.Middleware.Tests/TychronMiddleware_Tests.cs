using Microsoft.AspNetCore.Http;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.Middleware.Models;
using RK.Tychron.Middleware.Webhook;
using RK.Tychron.Middlewares;

namespace RK.Tychron.Middleware.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Middleware_should_add_header()
    {
        // Arrange:

        HttpContext ctx = new DefaultHttpContext();

        RequestDelegate next = (HttpContext hc) => Task.CompletedTask;

        var mw = new TychronMiddleware<SmsWebhookModel>();

        var requestModel = new SmsWebhookModel();

        var handler = new TestHandler<SmsWebhookModel>();

        // Act

        await mw.InvokeAsync(ctx, handler, null);

        // Assert - Part 1


    }
}

internal class TestHandler<T> : IWebhookHandler<T> where T : IValidationSubject
{
    public async Task Handle(T requestModel)
    {

        return;
    }
}