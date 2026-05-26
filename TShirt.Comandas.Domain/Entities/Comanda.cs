using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Comandas.Domain.Entities;

public class Comanda
{
    public Guid Id { get; private set; }
    public string NombreCliente { get; private set; }
    public string Vendedor { get; private set; }
    public string? Estampador { get; private set; }

    // Características de la prenda
    public string Talla { get; private set; }
    public TipoPrenda Tipo { get; private set; } // Aquí definimos si es Polera o Polerón
    public int ColorPrendaId { get; private set; }

    public EstadoComanda Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    // La lista de estampados está bloqueada. Nadie puede hacer _detalles.Add() desde afuera.
    private readonly List<DetalleEstampado> _detalles = new();
    public IReadOnlyCollection<DetalleEstampado> Detalles => _detalles.AsReadOnly();

    protected Comanda() { }

    // Constructor para crear una Comanda VÁLIDA desde el inicio
    public Comanda(string nombreCliente, string vendedor, string talla, TipoPrenda tipo, int colorPrendaId)
    {
        Id = Guid.NewGuid();
        NombreCliente = nombreCliente;
        Vendedor = vendedor;
        Talla = talla;
        Tipo = tipo;
        ColorPrendaId = colorPrendaId;
        Estado = EstadoComanda.Pendiente;
        FechaCreacion = DateTime.UtcNow;
    }

    // Única forma permitida de agregar un estampado a la comanda
    public void AgregarEstampado(string skuDiseno, string ubicacion, string tipoEstampado, int cantidad)
    {
        var detalle = new DetalleEstampado(skuDiseno, ubicacion, tipoEstampado, cantidad);
        _detalles.Add(detalle);
    }
}
