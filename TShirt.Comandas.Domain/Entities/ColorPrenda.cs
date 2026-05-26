using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Comandas.Domain.Entities;

public class ColorPrenda
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string CodigoHex { get; private set; }

    // Constructor vacío para Entity Framework
    protected ColorPrenda() { }

    public ColorPrenda(string nombre, string codigoHex)
    {
        Nombre = nombre;
        CodigoHex = codigoHex;
    }
}