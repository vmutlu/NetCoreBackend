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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) => (_categoryService) = (categoryService);

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetList();

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetById(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            var response = await _categoryService.Add(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromBody] Category category)
        {
            var response = await _categoryService.Delete(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            var response = await _categoryService.Update(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
