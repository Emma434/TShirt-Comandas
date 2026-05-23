using MediatR;
using TShirt.Comandas.Application;
using TShirt.Comandas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    // DIAGNÓSTICO: Esto listará en consola si MediatR está registrado
    var mediatorDescriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IMediator));
    if (mediatorDescriptor == null)
    {
        Console.WriteLine("¡ALERTA! IMediator NO está registrado en el contenedor.");
    }
    else
    {
        Console.WriteLine("IMediator detectado correctamente.");
    }

    // Diagnóstico rápido
    var serviceProvider = builder.Services.BuildServiceProvider();
    var mediator = serviceProvider.GetService<IMediator>();

    if (mediator == null)
    {
        throw new Exception("¡ERROR CRÍTICO! IMediator no pudo ser resuelto por el contenedor. Revisa ApplicationServiceRegistration.");
    }
    Console.WriteLine("IMediator resuelto correctamente.");

    var app = builder.Build();

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