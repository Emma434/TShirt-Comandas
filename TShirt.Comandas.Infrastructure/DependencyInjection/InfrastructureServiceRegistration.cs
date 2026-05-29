using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TShirt.Comandas.Application.Contracts; // Namespace exacto donde vive tu interfaz
using TShirt.Comandas.Infrastructure.Persistence;
using TShirt.Comandas.Infrastructure.Repositories; // Namespace exacto donde vive tu clase concreta

namespace TShirt.Comandas.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ComandasDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // ENLACE CRUCIAL: Amarra el contrato con la persistencia real
        services.AddScoped<IComandaRepository, ComandaRepository>();

        return services;
    }
}