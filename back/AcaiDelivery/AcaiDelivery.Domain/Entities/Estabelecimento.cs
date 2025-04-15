public class Estabelecimento : Usuario
{
    public string NomeFantasia { get; private set; }
    public string CNPJ { get; private set; }
    public string Endereco { get; private set; }
    public bool EstaAberto { get; private set; }
    public TimeSpan HorarioAbertura { get; private set; }
    public TimeSpan HorarioFechamento { get; private set; }
    public ICollection<Produto> Produtos { get; private set; } = new List<Produto>();

    public Estabelecimento(string nome, string email, string senha, string telefone, 
        string nomeFantasia, string cnpj, string endereco) 
        : base(nome, email, senha, telefone)
    {
        NomeFantasia = nomeFantasia;
        CNPJ = cnpj;
        Endereco = endereco;
        EstaAberto = false;
        HorarioAbertura = TimeSpan.FromHours(10);
        HorarioFechamento = TimeSpan.FromHours(22);
    }
    
    
    public void AbrirEstabelecimento() => EstaAberto = true;
    public void FecharEstabelecimento() => EstaAberto = false;
    public void AlterarHorarios(TimeSpan abertura, TimeSpan fechamento)
    {
        HorarioAbertura = abertura;
        HorarioFechamento = fechamento;
    }
}