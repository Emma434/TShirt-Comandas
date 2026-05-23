using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Comandas.Domain.Entities
{
    public class Comanda
    {
        public Guid Id { get; private set; }
        public string NombreCliente { get; private set; }
        public string DescripcionPedido { get; private set; }
        public EstadoComanda EstadoActual { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaActualizacion { get; private set; }

        // Un constructor privado obliga a usar el método "Crear"
        public Comanda() { }

        // Método de fábrica para crear una comanda limpia
        public static Comanda Crear(string nombreCliente, string descripcionPedido)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente)) throw new ArgumentException("El cliente es obligatorio");

            return new Comanda
            {
                Id = Guid.NewGuid(),
                NombreCliente = nombreCliente,
                DescripcionPedido = descripcionPedido,
                EstadoActual = EstadoComanda.Pendiente, // Nace obligatoriamente en Pendiente
                FechaCreacion = DateTime.UtcNow
            };
        }

        // Método para mover la máquina de estados
        public void CambiarEstado(EstadoComanda nuevoEstado)
        {
            // Aquí en el futuro pondremos reglas de negocio, 
            // ej: "No puedes pasar de Pendiente a Entregado sin pasar por EnPreparacion"

            EstadoActual = nuevoEstado;
            FechaActualizacion = DateTime.UtcNow;
        }
    }
}
