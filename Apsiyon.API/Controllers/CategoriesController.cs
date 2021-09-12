using Apsiyon.Business.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Services.Abstract;
using Apsiyon.Utilities.Results;
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
        private readonly IPaginationUriService _paginationUriService;
        public CategoriesController(ICategoryService categoryService, IPaginationUriService paginationUriService) => (_categoryService, _paginationUriService) = (categoryService, paginationUriService);

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromBody] PaginationQuery paginationQuery)
        {
            var response = await _categoryService.GetAllAsync(paginationQuery).ConfigureAwait(false);

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
