using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.Comandas.Application.Contracts; // ¡CRUCIAL! Esto ahora apuntará a la única interfaz viva en la solución
using TShirt.Comandas.Domain.Entities;
using TShirt.Comandas.Infrastructure.Persistence;
using TShirt.Comandas.Application.Contracts; // Debe consumir la de la Aplicación

namespace TShirt.Comandas.Infrastructure.Repositories;

// Al haber borrado el duplicado local, el compilador se verá obligado a enlazar con la capa de Aplicación
public class ComandaRepository : IComandaRepository
{
    private readonly ComandasDbContext _context;

    public ComandaRepository(ComandasDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comanda comanda, CancellationToken cancellationToken)
    {
        await _context.Comandas.AddAsync(comanda, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}