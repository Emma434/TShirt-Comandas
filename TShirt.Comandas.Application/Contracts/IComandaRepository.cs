using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Application.Contracts;

public interface IComandaRepository
{
    Task AddAsync(Comanda comanda, CancellationToken cancellationToken);

  
    Task<Comanda?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}