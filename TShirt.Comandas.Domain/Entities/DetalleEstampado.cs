using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Comandas.Domain.Entities;

public class DetalleEstampado
{
    public Guid Id { get; private set; }
    public Guid ComandaId { get; private set; }
    public string SkuDiseno { get; private set; }
    public string Ubicacion { get; private set; }
    public string TipoEstampado { get; private set; }
    public int Cantidad { get; private set; }

    protected DetalleEstampado() { }

    // El modificador 'internal' significa que solo la clase Comanda (dentro de este proyecto) puede crear detalles.
    internal DetalleEstampado(string skuDiseno, string ubicacion, string tipoEstampado, int cantidad)
    {
        Id = Guid.NewGuid();
        SkuDiseno = skuDiseno;
        Ubicacion = ubicacion;
        TipoEstampado = tipoEstampado;
        Cantidad = cantidad;
    }
}