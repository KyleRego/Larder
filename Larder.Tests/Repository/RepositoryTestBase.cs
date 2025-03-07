using Larder.Data;
using Larder.Models;
using Larder.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Larder.Tests.Repository;

public abstract class RepositoryTestBase
{
    private readonly SqliteConnection _connection;
    protected readonly AppDbContext _dbContext;
    protected static readonly string testUserId = TestUserData.TestUserId();

    public RepositoryTestBase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        ApplicationUser testUser = new()
        {
            Id = testUserId
        };

        _dbContext.Users.Add(testUser);
    }
}