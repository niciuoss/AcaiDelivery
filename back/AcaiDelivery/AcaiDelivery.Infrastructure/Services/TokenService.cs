public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(Usuario usuario)
    {
        var tipoUsuario = usuario is Cliente ? "Cliente" : "Estabelecimento";
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, tipoUsuario)
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: creds);
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}