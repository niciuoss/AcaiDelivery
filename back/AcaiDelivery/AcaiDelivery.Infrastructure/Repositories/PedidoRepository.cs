public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(AcaiDeliveryDbContext context) : base(context) { }
    
    public async Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(Guid clienteId)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Adicionais)
                    .ThenInclude(a => a.Adicional)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .Where(p => p.ClienteId == clienteId)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Pedido>> GetPedidosByEstabelecimentoIdAsync(Guid estabelecimentoId)
    {
        return await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Adicionais)
                    .ThenInclude(a => a.Adicional)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .Where(p => p.EstabelecimentoId == estabelecimentoId)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();
    }
}