using Apsiyon.Business.Abstract;
using Apsiyon.Entities;
using Apsiyon.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Apsiyon.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) => (_categoryService) = (categoryService);

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(GeneralFilter generalFilter)
        {
            var response = await _categoryService.GetAllAsync(generalFilter).ConfigureAwait(false);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            var response = await _categoryService.AddAsync(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            var response = await _categoryService.UpdateAsync(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
