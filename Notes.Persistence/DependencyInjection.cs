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
        var connectionString1 = configuration["DbConnection"];
        var connectionString2 = configuration["AuthDbConnection"];

        services.AddDbContext<NotesDbContext>(options => options.UseSqlite(connectionString1));
        services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>());

        services.AddDbContext<UserDbContext>(optionsAction => optionsAction.UseSqlite(connectionString2));
        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

        return services;
    }
}
