using Apsiyon.Business.Abstract;
using Apsiyon.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apsiyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var response = _productService.GetList();

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }


        [HttpGet("getListByCategory/{categoryId}")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var response = _productService.GetListProductCategory(categoryId);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var response = _productService.GetById(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] Product product)
        {
            var response = _productService.Add(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromBody] Product product)
        {
            var response = _productService.Delete(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] Product product)
        {
            var response = _productService.Update(product);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
