using IdentityJwt.Entities;
using IdentityJwt.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //-> impede que qualquer pessoa sem authenticacao acesse esse endpoint
    public class ProductsController : ControllerBase
    {
        private readonly InterfaceProduct _InterfaceProduct;

        public ProductsController(InterfaceProduct InterfaceProduct)
        {
            _InterfaceProduct = InterfaceProduct;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<object> List()
        {
            return await _InterfaceProduct.List();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<object> Add(ProductModel product)
        {
            try
            {
                await _InterfaceProduct.Add(product);
            }
            catch (Exception ERRO)
            {

            }

            return Task.FromResult("OK");
        }

        [HttpPut]
        [Produces("application/json")]
        public async Task<object> Update(ProductModel product)
        {
            try
            {
                await _InterfaceProduct.Update(product);
            }
            catch (Exception ERRO)
            {

            }

            return Task.FromResult("OK");
        }


        [HttpGet("GetEntityById")]
        [Produces("application/json")]
        public async Task<object> GetEntityById(int id)
        {
            return await _InterfaceProduct.GetEntityById(id);
        }

        [HttpDelete]
        [Produces("application/json")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var product = await _InterfaceProduct.GetEntityById(id);

                await _InterfaceProduct.Delete(product);

            }
            catch (Exception ERRO)
            {
                return false;
            }

            return true;

        }

    }
}
