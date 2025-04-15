public class ItemPedidoAdicional
{
    public Guid Id { get; private set; }
    public Guid ItemPedidoId { get; private set; }
    public ItemPedido ItemPedido { get; private set; }
    public Guid AdicionalId { get; private set; }
    public Adicional Adicional { get; private set; }
    public decimal Preco { get; private set; }

    public ItemPedidoAdicional(Guid itemPedidoId, Guid adicionalId, decimal preco)
    {
        Id = Guid.NewGuid();
        ItemPedidoId = itemPedidoId;
        AdicionalId = adicionalId;
        Preco = preco;
    }
}