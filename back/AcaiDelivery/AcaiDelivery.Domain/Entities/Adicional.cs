public class Adicional
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public bool Disponivel { get; private set; }
    public Guid EstabelecimentoId { get; private set; }
    public Estabelecimento Estabelecimento { get; private set; }

    public Adicional(string nome, decimal preco, Guid estabelecimentoId)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Preco = preco;
        Disponivel = true;
        EstabelecimentoId = estabelecimentoId;
    }
    
    public void AlterarDisponibilidade(bool disponivel) => Disponivel = disponivel;
    public void AtualizarPreco(decimal novoPreco) => Preco = novoPreco;
}