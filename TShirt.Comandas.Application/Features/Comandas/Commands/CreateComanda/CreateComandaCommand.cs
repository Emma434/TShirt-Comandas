using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TShirt.Comandas.Application.Features.Comandas.Commands.CreateComanda;

// El "Command" es la estructura de datos que esperamos recibir desde el frontend (Swagger/Postman)
public record CreateComandaCommand(
    string NombreCliente,
    string Vendedor,
    string Talla,
    int TipoPrendaId, // Recibiremos 1 para Polera, 2 para Poleron
    int ColorPrendaId,
    List<EstampadoDto> Estampados // La lista de diseños que lleva esta prenda
) : IRequest<Guid>;

// DTO (Data Transfer Object) para los detalles
public record EstampadoDto(
    string SkuDiseno,
    string Ubicacion,
    string TipoEstampado,
    int Cantidad
);