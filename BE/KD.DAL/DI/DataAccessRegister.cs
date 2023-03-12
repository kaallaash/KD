using KD.DAL.Context;
using KD.DAL.Entities;
using KD.DAL.Interfaces.Repositories;
using KD.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KD.DAL.DI;

public static class DataAccessRegister
{
    public static void AddDataContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDrawingRepository<DrawingEntity>, DrawingRepository>();
        services.AddScoped<IUniversityRepository<UniversityEntity>, UniversityRepository>();

        services.AddDbContext<DatabaseContext>(op =>
        {
            op.UseSqlServer(
                configuration.GetConnectionString("KalashnikovDrawings_Db"));
        });
    }
}