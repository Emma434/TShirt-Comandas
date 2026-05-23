using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.Comandas.Application.Contracts;
using TShirt.Comandas.Domain.Entities;
using TShirt.Comandas.Infrastructure.Persistence; // Ajusta según donde esté tu AppDbContext

namespace TShirt.Comandas.Infrastructure.Repositories;

public class ComandaRepository : IComandaRepository
{
    private readonly ComandasDbContext _context;

    public ComandaRepository(ComandasDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comanda comanda)
    {
        await _context.Comandas.AddAsync(comanda);
        await _context.SaveChangesAsync();
    }
}