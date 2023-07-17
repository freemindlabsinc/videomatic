﻿namespace Company.Videomatic.Infrastructure.Data;

public abstract class VideoMaticDbContextFactory<TDBCONTEXT> : IDbContextFactory<VideomaticDbContext>
    where TDBCONTEXT : VideomaticDbContext
{    
    public VideoMaticDbContextFactory(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    protected virtual string GetConnectionString()
    {
        // Looks for the connection string Videomatic.<ProviderName> [e.g. SqlServer, Sqlite, etc.]
        var dbCtxNamePrefix = typeof(TDBCONTEXT).Name.Replace(nameof(VideomaticDbContext), string.Empty);
        var connectionName = $"Videomatic.{dbCtxNamePrefix}";
        //var connString = Configuration.GetConnectionString(connectionName) ?? throw new Exception($"Required connection string '{connectionName}' missing.");
        var connString = "Server=localhost;Database=Videomatic_IntegrationTests;User Id=sa;Password=G7i5z5mo;TrustServerCertificate=True";

        return connString;
    }

    protected virtual void ConfigureContext(string connectionString, DbContextOptionsBuilder builder)
    {
        builder.EnableSensitiveDataLogging();
    }

    public VideomaticDbContext CreateDbContext()
    {
        var connString = GetConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder();
        ConfigureContext(connString, optionsBuilder);
        
        return (VideomaticDbContext)Activator.CreateInstance(
            typeof(TDBCONTEXT), 
            optionsBuilder.Options)!;
    }
}
