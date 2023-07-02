﻿using Company.Videomatic.Application.Behaviors;
using Company.Videomatic.Infrastructure.Data.Seeder;
using Microsoft.Extensions.Configuration;

// This is required so I can mark validators as 'internal' (i.e. instead of public) and still be able to access them from the test project.
// See https://learn.microsoft.com/en-us/dotnet/standard/assembly/friend for more information.

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Application.Tests")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{    
    /// <summary>
    /// Adds 
    /// -MediatR
    /// -IPipelineBehavior for logging and validation
    /// -AutoMapper
    /// -FluentValidation validators
    /// -IDataSeeder
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddVideomaticApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions

        // Services
        //services.AddValidatorsFromAssembly(typeof(LoggingBehaviour<,>).Assembly);
        services.AddMediatR(cfg =>        
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.FullName?.Contains(".Videomatic.") ?? false);
            
            var allAssemblies = assemblies.ToArray();
            cfg.RegisterServicesFromAssemblies(allAssemblies);
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddAutoMapper((cfg) =>
        {
        },
        AppDomain.CurrentDomain.GetAssemblies());


        services.AddValidatorsFromAssembly(
            typeof(PageResult<>).Assembly, // The only validators are in this assembly
            includeInternalTypes: true // TODO: Seems useless? Maybe it will surface in the app?
            );

        services.AddTransient<IDataSeeder, DataSeeder>();


        return services;
    }       
}