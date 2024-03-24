using IdentityJwt.Config;
using IdentityJwt.Entities;
using IdentityJwt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace IdentityJwt.Repositories
{
    public class RepositoryProduct : InterfaceProduct
    {
        //Observacao importante nessa classe!!!
        //Repare que estamos utilizando `Using` para abrir e fechar a conexao com o banco de dados (ou Criar a variavel de instancia que abre conexao com o banco de dados)
        //O using aqui esta sendo utilizado para garantir que a conexao com o banco de dados seja fechada apos a execucao do metodo
        //E como um dispose no flutter (para evitar o vazamento da memoria ou de recursos como no caso do banco de dados fica aberto sem necessidade)
        private readonly DbContextOptions<ContextBase> _contextOptions;

        public RepositoryProduct(DbContextOptions<ContextBase> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task Add(ProductModel objeto)
        {
            using (var context = new ContextBase(_contextOptions))
            {
                await context.Set<ProductModel>().AddAsync(objeto);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(ProductModel objeto)
        {
            using (var context = new ContextBase(_contextOptions))
            {
                context.Set<ProductModel>().Remove(objeto);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ProductModel> GetEntityById(int id)
        {
            using (var context = new ContextBase(_contextOptions))
            {
                return await context.Set<ProductModel>().FindAsync(id);
            }
        }

        public async Task<List<ProductModel>> List()
        {
            using (var context = new ContextBase(_contextOptions))
            {
                return await context.Set<ProductModel>().ToListAsync();
            }
        }

        public async Task Update(ProductModel objeto)
        {
            using (var context = new ContextBase(_contextOptions))
            {
                context.Set<ProductModel>().Update(objeto);
                await context.SaveChangesAsync();
            }
        }
    }
}
