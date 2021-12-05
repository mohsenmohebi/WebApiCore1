using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApiWithToken;


public class JwtAuthenticationManager : IJwtAuthenticationManger
{

    private readonly string key;
    public JwtAuthenticationManager(string key){
        this.key = key;
    }
    private IDictionary<string,string> Users= new Dictionary<string,string>(){
        {"Mohsen","123"},
        {"Hamed","345"},
        {"Hesam","567"},
        {"Ryan","789"},
    };
    public string Authenticate(string userName, string password){
        if(Users.ContainsKey(userName) && Users.Values.Contains(password))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDecriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256Signature 
                    )
            };
            var token =  tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
        return null;
        
    }

}