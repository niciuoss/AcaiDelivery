public abstract class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataCadastro { get; private set; }

    protected Usuario(string nome, string email, string senha, string telefone)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
        Telefone = telefone;
        DataCadastro = DateTime.Now;
    }

}