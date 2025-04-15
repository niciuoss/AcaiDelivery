public class ItemPedido
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Pedido Pedido { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal PrecoTotal { get; private set; }
    public ICollection<ItemPedidoAdicional> Adicionais { get; private set; } = new List<ItemPedidoAdicional>();

    public ItemPedido(Guid pedidoId, Guid produtoId, int quantidade, decimal precoUnitario)
    {
        Id = Guid.NewGuid();
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        PrecoTotal = quantidade * precoUnitario;
    }
    
    public void AdicionarAdicional(ItemPedidoAdicional adicional)
    {
        Adicionais.Add(adicional);
        PrecoTotal += adicional.Preco;
    }
}