namespace TShirt.Comandas.Domain.Entities;

public class Comanda
{
    public Guid Id { get; private set; }
    public string NombreCliente { get; private set; }


    public string Origen { get; private set; }

    // Quien ejecuta el trabajo (estampador, barista, técnico)
    public string? ResponsableEjecucion { get; private set; }

    public EstadoComanda Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private readonly List<ItemComanda> _items = new();
    public IReadOnlyCollection<ItemComanda> Items => _items.AsReadOnly();

    protected Comanda() { }

    // Constructor actualizado con la abstracción
    public Comanda(string nombreCliente, string origen)
    {
        Id = Guid.NewGuid();
        NombreCliente = nombreCliente;
        Origen = origen;
        Estado = EstadoComanda.Pendiente;
        FechaCreacion = DateTime.UtcNow;
    }

    public void AgregarItem(string skuProducto, string descripcion, int cantidad, string? notasConfiguracion)
    {
        var item = new ItemComanda(skuProducto, descripcion, cantidad, notasConfiguracion);
        _items.Add(item);
    }
}