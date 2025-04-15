public class ProdutoService : IProdutoService
{
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IMapper _mapper;
    
    public ProdutoService(IRepository<Produto> produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    
    public async Task<ProdutoViewModel> CriarProdutoAsync(CriarProdutoViewModel model)
    {
        var produto = new Produto(
            model.Nome,
            model.TamanhoEmMl,
            model.Preco,
            model.Descricao,
            model.EstabelecimentoId);
            
        await _produtoRepository.AddAsync(produto);
        return _mapper.Map<ProdutoViewModel>(produto);
    }
    
    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorEstabelecimentoAsync(Guid estabelecimentoId)
    {
        var produtos = await _produtoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(
            produtos.Where(p => p.EstabelecimentoId == estabelecimentoId && p.Disponivel));
    }
    
    // Outros métodos
}
