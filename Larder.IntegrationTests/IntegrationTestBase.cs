using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Larder.Models;
using Larder.Repository.Impl;

namespace Larder.IntegrationTests;

public abstract class IntegrationTestBase : IClassFixture<TestAppFactory<Program>>
{
    protected readonly HttpClient _client;
    protected readonly IServiceScope _scope;
    protected readonly AppDbContext _dbContext;
    protected readonly string _testUserEmail = "testuser@example.com";
    protected readonly string _testUserPassword = "Test1234!";
    protected ApplicationUser _testUser = null!;

    protected IntegrationTestBase(TestAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = true
        });

        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

        InitializeAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeAsync()
    {
        var userManager = _scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        _testUser = (await userManager.FindByEmailAsync(_testUserEmail))!;

        if (_testUser == null)
        {
            _testUser = new ApplicationUser
            {
                UserName = _testUserEmail,
                Email = _testUserEmail
            };
            IdentityResult result = await userManager.CreateAsync(_testUser, _testUserPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create test user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var loginPayload = new { email = _testUserEmail, password = _testUserPassword };

        var loginResponse = await _client.PostAsJsonAsync("/login?useCookies=true", loginPayload);
        loginResponse.Headers.TryGetValues("Set-Cookie", out var cookies);

        if (cookies == null)
            throw new Exception("No Set-Cookie header received!");

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    ~IntegrationTestBase()
    {
        _scope.Dispose();
    }
}
