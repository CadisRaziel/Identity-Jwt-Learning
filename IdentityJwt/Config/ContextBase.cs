using IdentityJwt.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityJwt.Config
{
    public class ContextBase : IdentityDbContext<ApplicationUser> //aqui na tipagem eu passo a entitade que eu herdei de IdentityUser
    {

        //Repare que a entitdade ApplicationUser foi passada como tipo generico para o IdentityDbContext(entao eu nao passo aqui)
        public DbSet<ProductModel> product { get; set; }
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(key => key.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
