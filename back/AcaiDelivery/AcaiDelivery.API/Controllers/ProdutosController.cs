[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    
    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Estabelecimento")]
    public async Task<ActionResult<ProdutoViewModel>> CriarProduto(CriarProdutoViewModel model)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId != model.EstabelecimentoId.ToString())
            return Forbid();
            
        var resultado = await _produtoService.CriarProdutoAsync(model);
        return CreatedAtAction(nameof(ObterProdutoPorId), new { id = resultado.Id }, resultado);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoViewModel>> ObterProdutoPorId(Guid id)
    {
        var produto = await _produtoService.ObterProdutoPorIdAsync(id);
        if (produto == null) return NotFound();
        
        return Ok(produto);
    }
    
    [HttpGet("estabelecimento/{estabelecimentoId}")]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterProdutosPorEstabelecimento(Guid estabelecimentoId)
    {
        var produtos = await _produtoService.ObterProdutosPorEstabelecimentoAsync(estabelecimentoId);
        return Ok(produtos);
    }
    
  
}