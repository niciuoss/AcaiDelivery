public class Produto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int TamanhoEmMl { get; private set; }
    public decimal Preco { get; private set; }
    public string Descricao { get; private set; }
    public bool Disponivel { get; private set; }
    public Guid EstabelecimentoId { get; private set; }
    public Estabelecimento Estabelecimento { get; private set; }

    public Produto(string nome, int tamanhoEmMl, decimal preco, string descricao, Guid estabelecimentoId)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        TamanhoEmMl = tamanhoEmMl;
        Preco = preco;
        Descricao = descricao;
        Disponivel = true;
        EstabelecimentoId = estabelecimentoId;
    }
    
    public void AlterarDisponibilidade(bool disponivel) => Disponivel = disponivel;
    public void AtualizarPreco(decimal novoPreco) => Preco = novoPreco;
}