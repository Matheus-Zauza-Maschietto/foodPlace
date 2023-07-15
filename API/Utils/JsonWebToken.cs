using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utils;

public class JsonWebToken
{
    public string GerarToken(ClaimsIdentity ClaimsParaToken, IConfiguration _configuration)
    {
        var informacoesParaToken = BuscarInformacoesDoToken(_configuration);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ClaimsParaToken,
            SigningCredentials = informacoesParaToken.SigningCredentials,
            Audience = informacoesParaToken.Audience,
            Issuer = informacoesParaToken.Issuer,
            Expires = informacoesParaToken.Expires
        };

        return _gerarTokenString(tokenDescriptor);
    }

    private (SigningCredentials SigningCredentials, string Audience, string Issuer, DateTime Expires) BuscarInformacoesDoToken(IConfiguration _configuration)
    {
        var signingCredentials = _gerarCredenciais(_configuration["JwtBearerTokenSettings:SecretKey"]);
        double lifeTimeInMinutes = double.Parse(_configuration["JwtBearerTokenSettings:LifeTimeInMinutes"]);
        string audience = _configuration["JwtBearerTokenSettings:Audience"];
        string issuer = _configuration["JwtBearerTokenSettings:Issuer"];
        DateTime expires = DateTime.UtcNow.AddMinutes(lifeTimeInMinutes);

        return (signingCredentials, audience, issuer, expires);
    }

    private SigningCredentials _gerarCredenciais(string secretKey)
    {
        byte[] key = Encoding.ASCII.GetBytes(secretKey);
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

        return signingCredentials;
    }

    private string _gerarTokenString(SecurityTokenDescriptor descriptor)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        var pretoken = TokenHandler.CreateToken(descriptor);
        return TokenHandler.WriteToken(pretoken);
    }


}


