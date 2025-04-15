public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<Adicional> _adicionalRepository;
    private readonly IMapper _mapper;
    
    public PedidoService(
        IPedidoRepository pedidoRepository, 
        IRepository<Produto> produtoRepository,
        IRepository<Adicional> adicionalRepository,
        IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _adicionalRepository = adicionalRepository;
        _mapper = mapper;
    }
    
    public async Task<PedidoViewModel> CriarPedidoAsync(CriarPedidoViewModel model)
    {
        // Criar o pedido principal
        var pedido = new Pedido(
            model.ClienteId,
            model.EstabelecimentoId,
            model.TipoFrete,
            model.ValorFrete,
            model.FormaPagamento);
            
        
        foreach (var itemModel in model.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemModel.ProdutoId);
            if (produto == null) throw new Exception($"Produto não encontrado: {itemModel.ProdutoId}");
            
            var itemPedido = new ItemPedido(
                pedido.Id,
                produto.Id,
                itemModel.Quantidade,
                produto.Preco);
                
            
            foreach (var adicionalId in itemModel.AdicionaisIds)
            {
                var adicional = await _adicionalRepository.GetByIdAsync(adicionalId);
                if (adicional == null) throw new Exception($"Adicional não encontrado: {adicionalId}");
                
                var itemAdicional = new ItemPedidoAdicional(
                    itemPedido.Id,
                    adicional.Id,
                    adicional.Preco);
                    
                itemPedido.AdicionarAdicional(itemAdicional);
            }
            
            pedido.AdicionarItem(itemPedido);
        }
        
        await _pedidoRepository.AddAsync(pedido);
        return _mapper.Map<PedidoViewModel>(pedido);
    }
    
    public async Task<PedidoViewModel> AtualizarStatusPedidoAsync(Guid pedidoId, StatusPedido novoStatus)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
        if (pedido == null) throw new Exception($"Pedido não encontrado: {pedidoId}");
        
        pedido.AtualizarStatus(novoStatus);
        await _pedidoRepository.UpdateAsync(pedido);
        
        return _mapper.Map<PedidoViewModel>(pedido);
    }
    
}