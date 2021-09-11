using Apsiyon.Business.Abstract;
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
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetList();

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

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var response = await _productService.GetById(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var response = await _productService.Add(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromBody] Product product)
        {
            var response = await _productService.Delete(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var response = await _productService.Update(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
