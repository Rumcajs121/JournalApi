using FluentValidation;
using Journal.Application;
using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Commands.EditJournal;
using Journal.Application.Dtos;
using Journal.Infrastructure.Middleware;
using Journal.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.Infrastructure.Extensions;

public static class ServiceExtensionsColletions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JournalDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("JournalApiDb")));
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddSingleton<ErrorHandlingMiddleware>();
    }
}