﻿using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Integration.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());

        services.AddApplication(cfg);
        services.AddYouTubeDrivers(cfg);
        services.AddSqlServerDriver(cfg);        
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}