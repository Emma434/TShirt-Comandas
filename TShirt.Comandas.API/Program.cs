using MediatR;
using TShirt.Comandas.Application;
using TShirt.Comandas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // 1. Registrar los servicios de las capas inferiores
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    // ?? MOVER LA SONDA AQUÍ: Justo antes de builder.Build()
    Console.WriteLine("\n === INICIANDO AUDITORÍA FÍSICA DE ENSAMBLADOS ===");

    // Evaluar qué tiene el contenedor registrado
    var servicioRegistrado = builder.Services.FirstOrDefault(d => d.ServiceType.FullName == "TShirt.Comandas.Application.Contracts.IComandaRepository");
    if (servicioRegistrado != null)
    {
        Console.WriteLine($"   [DI CONTAINER] La interfaz registrada en IoC proviene de:");
        Console.WriteLine($"   -> Ruta Física: {servicioRegistrado.ServiceType.Assembly.Location}");
    }
    else
    {
        Console.WriteLine("   [DI CONTAINER] ¡ALERTA! El contrato IComandaRepository NO se encontró en la lista.");
    }

    // Evaluar qué está leyendo el Handler de MediatR
    var tipoHandler = typeof(TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda.CreateComandaCommandHandler);
    var parametroConstructor = tipoHandler.GetConstructors().First().GetParameters().FirstOrDefault(p => p.ParameterType.Name.Contains("Repository"));
    if (parametroConstructor != null)
    {
        Console.WriteLine($"   [MEDIATR HANDLER] La interfaz que exige el constructor del Handler proviene de:");
        Console.WriteLine($"   -> Ruta Física: {parametroConstructor.ParameterType.Assembly.Location}");
    }

    if (servicioRegistrado != null && parametroConstructor != null)
    {
        Console.WriteLine($"\n   ¿Es exactamente el mismo tipo físico en la memoria del CLR?: {servicioRegistrado.ServiceType == parametroConstructor.ParameterType}");
    }
    Console.WriteLine(" === FIN DE LA AUDITORÍA FÍSICA ===\n");


    // 2. Aquí el framework bloquea e intenta construir el árbol
    var app = builder.Build();

    app.UseMiddleware<TShirt.Comandas.API.Middleware.ExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (System.Reflection.ReflectionTypeLoadException ex)
{
    foreach (var loaderEx in ex.LoaderExceptions)
    {
        Console.WriteLine($"LOADER ERROR: {loaderEx?.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"MAIN ERROR: {ex.Message}");
}