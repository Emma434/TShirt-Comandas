namespace TShirt.Comandas.Domain.Entities;

public class ItemComanda
{
    public Guid Id { get; private set; }
    public string SkuProducto { get; private set; } // Ej: "POL-NEG-L" o "CAFE-LATTE"
    public string DescripcionProducto { get; private set; } // "Polera Negra Talla L"
    public int Cantidad { get; private set; }

    // EL TRUCO DEL SAAS: Un campo de configuración dinámica.
    // Aquí puedes guardar un JSON o texto como: "Estampado en espalda" o "Sin lactosa, extra caliente"
    public string? NotasConfiguracion { get; private set; }

    protected ItemComanda() { }

    internal ItemComanda(string skuProducto, string descripcionProducto, int cantidad, string? notasConfiguracion)
    {
        Id = Guid.NewGuid();
        SkuProducto = skuProducto;
        DescripcionProducto = descripcionProducto;
        Cantidad = cantidad;
        NotasConfiguracion = notasConfiguracion;
    }
}