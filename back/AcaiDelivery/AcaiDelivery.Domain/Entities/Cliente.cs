public class Cliente : Usuario
{
    public string Endereco { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string CEP { get; private set; }
    public ICollection<Pedido> Pedidos { get; private set; } = new List<Pedido>();

    public Cliente(string nome, string email, string senha, string telefone, 
        string endereco, string complemento, string bairro, string cidade, string cep) 
        : base(nome, email, senha, telefone)
    {
        Endereco = endereco;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        CEP = cep;
    }
    
}