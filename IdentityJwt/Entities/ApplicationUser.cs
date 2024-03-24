using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityJwt.Entities
{
    public class ApplicationUser : IdentityUser //-> Lembrar de sempre criar a tabela user com essa heranca
    {
        [Column("USR_RG")]
        public string RG { get; set; }
    }
}
