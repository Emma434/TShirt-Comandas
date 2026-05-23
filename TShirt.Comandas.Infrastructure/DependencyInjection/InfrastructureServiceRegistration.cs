using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Infrastructure.Persistence;
using TShirt.Comandas.Infrastructure.Repositories;

namespace TShirt.Comandas.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Registrar el DbContext
        services.AddDbContext<ComandasDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // 2. Registrar los repositorios
        services.AddScoped<IComandaRepository, ComandaRepository>();

        return services;
    }
}