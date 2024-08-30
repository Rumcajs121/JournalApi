using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.Infrastructure.Extensions;

public static class ServiceExtensionsColletions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JournalDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Connectionstring:JournalApiDb")));
    }
}