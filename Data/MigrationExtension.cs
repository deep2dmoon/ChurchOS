using Microsoft.EntityFrameworkCore;

namespace htmos.data;

public static class MigrationExtension
{
    public static void ContextMigrator(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();
    }
}