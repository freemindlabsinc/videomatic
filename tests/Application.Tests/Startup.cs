﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Tests;

public class Startup
{
    public Startup() { }

    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(x => x.AddConsole());
                services.AddVideomaticApplication(context.Configuration);
            })
            .ConfigureHostConfiguration(builder =>
            {

            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                // IMPORTANT: if I don't do this here the IConfiguration and IServiceProvider 
                // are empty or default and tests fail later (e.g. read conn strings).
                // See https://github.com/pengweiqhca/Xunit.DependencyInjection#how-to-inject-iconfiguration-or-ihostenvironment-into-startup
                var cfg = new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();

                builder.AddConfiguration(cfg);
            });
    }
}