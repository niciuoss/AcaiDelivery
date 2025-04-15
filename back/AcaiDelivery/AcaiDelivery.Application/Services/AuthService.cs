
public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;
    
    public AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService,
        IPasswordHashService passwordHashService,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }
    
    public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel model)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(model.Email);
        if (usuario == null)
            throw new BusinessRuleException("Email ou senha inválidos");
            
        if (!_passwordHashService.VerifyPassword(model.Senha, usuario.Senha))
            throw new BusinessRuleException("Email ou senha inválidos");
            
        var token = _tokenService.GenerateToken(usuario);
        
        return new LoginResponseViewModel
        {
            Token = token,
            Usuario = _mapper.Map<UsuarioViewModel>(usuario)
        };
    }
    
    public async Task<UsuarioViewModel> RegistrarClienteAsync(RegistrarClienteViewModel model)
    {
        var existente = await _usuarioRepository.GetByEmailAsync(model.Email);
        if (existente != null)
            throw new BusinessRuleException("Email já registrado");
            
        var senhaHash = _passwordHashService.HashPassword(model.Senha);
        
        var cliente = new Cliente(
            model.Nome,
            model.Email,
            senhaHash,
            model.Telefone,
            model.Endereco,
            model.Complemento,
            model.Bairro,
            model.Cidade,
            model.CEP);
            
        await _usuarioRepository.AddAsync(cliente);
        
        return _mapper.Map<UsuarioViewModel>(cliente);
    }
    
    public async Task<UsuarioViewModel> RegistrarEstabelecimentoAsync(RegistrarEstabelecimentoViewModel model)
    {
        var existente = await _usuarioRepository.GetByEmailAsync(model.Email);
        if (existente != null)
            throw new BusinessRuleException("Email já registrado");
            
        var senhaHash = _passwordHashService.HashPassword(model.Senha);
        
        var estabelecimento = new Estabelecimento(
            model.Nome,
            model.Email,
            senhaHash,
            model.Telefone,
            model.NomeFantasia,
            model.CNPJ,
            model.Endereco);
            
        await _usuarioRepository.AddAsync(estabelecimento);
        
        return _mapper.Map<UsuarioViewModel>(estabelecimento);
    }
}