using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Comandas.Domain.Entities
{
    public enum EstadoComanda
    {
        Pendiente = 1,
        EnPreparacion = 2,
        Terminado = 3,
        Entregado = 4
    }
}
