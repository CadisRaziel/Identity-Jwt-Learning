using System.IdentityModel.Tokens.Jwt;

namespace IdentityJwt.Token
{
    public class TokenJwt
    {        
        private JwtSecurityToken token;

        internal TokenJwt(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo; //-> validacao do token
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token); //-> valor do token
    }
}
