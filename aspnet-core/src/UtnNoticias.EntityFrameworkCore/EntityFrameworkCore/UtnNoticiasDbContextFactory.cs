using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UtnNoticias.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class UtnNoticiasDbContextFactory : IDesignTimeDbContextFactory<UtnNoticiasDbContext>
{
    public UtnNoticiasDbContext CreateDbContext(string[] args)
    {
        UtnNoticiasEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UtnNoticiasDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new UtnNoticiasDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../UtnNoticias.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
