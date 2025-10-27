using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Api.Data;

namespace Todo.Api.Tests.Web;

public class SqliteAppFactory : WebApplicationFactory<Program>
{
    private DbConnection? _conn;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var toRemove = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (toRemove is not null) services.Remove(toRemove);

            _conn = new SqliteConnection("DataSource=:memory:");
            _conn.Open();

            services.AddDbContext<AppDbContext>(opts => opts.UseSqlite(_conn!));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _conn?.Dispose();
    }
}
