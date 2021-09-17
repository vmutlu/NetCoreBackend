using Apsiyon.Business.Abstract;
using Apsiyon.Entities;
using Apsiyon.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Apsiyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => (_productService) = (productService);

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(GeneralFilter generalFilter)
        {
            var response = await _productService.GetAllAsync(generalFilter).ConfigureAwait(false);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }


        [HttpGet("getListByCategory/{categoryId}")]
        public async Task<IActionResult> GetListByCategory(int categoryId)
        {
            var response = await _productService.GetListProductCategory(categoryId);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _productService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var response = await _productService.AddAsync(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var response = await _productService.UpdateAsync(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
