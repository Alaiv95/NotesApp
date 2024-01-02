using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;

namespace Notes.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection 
        services, IConfiguration configuration)
    {
        var mainDbConnection = configuration["DbConnection"];
        var authDbConnection = configuration["AuthDbConnection"];

        services.AddDbContext<NotesDbContext>(options => options.UseSqlite(mainDbConnection));
        services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>());

        services.AddDbContext<UserDbContext>(optionsAction => optionsAction.UseSqlite(authDbConnection));
        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

        return services;
    }
}
