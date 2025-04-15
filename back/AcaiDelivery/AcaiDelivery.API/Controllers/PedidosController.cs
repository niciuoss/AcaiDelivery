[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    
    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<ActionResult<PedidoViewModel>> CriarPedido(CriarPedidoViewModel model)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId != model.ClienteId.ToString())
            return Forbid();
            
        var resultado = await _pedidoService.CriarPedidoAsync(model);
        return CreatedAtAction(nameof(ObterPedidoPorId), new { id = resultado.Id }, resultado);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoViewModel>> ObterPedidoPorId(Guid id)
    {
        var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);
        if (pedido == null) return NotFound();
        
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        
        if (role == "Cliente" && pedido.ClienteId.ToString() != usuarioId)
            return Forbid();
            
        if (role == "Estabelecimento" && pedido.EstabelecimentoId.ToString() != usuarioId)
            return Forbid();
            
        return Ok(pedido);
    }
    
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Estabelecimento")]
    public async Task<ActionResult<PedidoViewModel>> AtualizarStatusPedido(Guid id, [FromBody] AtualizarStatusPedidoViewModel model)
    {
        var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);
        if (pedido == null) return NotFound();
        
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (pedido.EstabelecimentoId.ToString() != usuarioId)
            return Forbid();
            
        var resultado = await _pedidoService.AtualizarStatusPedidoAsync(id, model.NovoStatus);
        return Ok(resultado);
    }
    
}