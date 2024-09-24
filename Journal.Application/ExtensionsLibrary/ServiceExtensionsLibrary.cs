using FluentValidation;
using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Commands.EditJournal;
using log4net;
using log4net.Config;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.Application;

public static class ServiceExtensionsLibrary
{
    public static void AddLog4Net<T>(this IServiceCollection services)
    {
        // Add services to the container.
        var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        services.AddSingleton(LogManager.GetLogger(typeof(T)));
    }
    public static void  AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddValidatorsFromAssembly(typeof(EditJournalValidator).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        
    }
}