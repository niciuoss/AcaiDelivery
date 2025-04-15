public class Pedido
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public Guid EstabelecimentoId { get; private set; }
    public Estabelecimento Estabelecimento { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataEntrega { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusPedido Status { get; private set; }
    public string TipoFrete { get; private set; } // "Normal" ou "Rápido"
    public decimal ValorFrete { get; private set; }
    public string FormaPagamento { get; private set; } // "Crédito", "Débito", "PIX"
    public ICollection<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();

    public Pedido(Guid clienteId, Guid estabelecimentoId, string tipoFrete, decimal valorFrete, string formaPagamento)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        EstabelecimentoId = estabelecimentoId;
        DataCriacao = DateTime.Now;
        Status = StatusPedido.Criado;
        TipoFrete = tipoFrete;
        ValorFrete = valorFrete;
        FormaPagamento = formaPagamento;
        ValorTotal = valorFrete; 
    }
    
    public void AdicionarItem(ItemPedido item)
    {
        Itens.Add(item);
        RecalcularValorTotal();
    }
    
    public void AtualizarStatus(StatusPedido novoStatus)
    {
        Status = novoStatus;
        if (novoStatus == StatusPedido.Entregue)
            DataEntrega = DateTime.Now;
    }
    
    private void RecalcularValorTotal()
    {
        ValorTotal = ValorFrete + Itens.Sum(i => i.PrecoTotal);
    }
}