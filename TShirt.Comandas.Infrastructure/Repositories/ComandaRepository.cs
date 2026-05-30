using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Domain.Entities;
using TShirt.Comandas.Infrastructure.Persistence;

namespace TShirt.Comandas.Infrastructure.Repositories;

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

    // IMPLEMENTACIÓN DE LECTURA OPTIMIZADA (SaaS Grade)
    public async Task<Comanda?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Comandas
            .AsNoTracking() // Mitiga la sobre-validación y acelera la lectura en memoria
            .Include(c => c.Items) // Carga el grafo completo de ítems e hidrata sus JsonDocument
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}