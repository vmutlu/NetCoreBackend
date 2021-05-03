using Apsiyon.Business.Abstract;
using Apsiyon.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apsiyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var response = _categoryService.GetList();

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var response = _categoryService.GetById(id);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] Category category)
        {
            var response = _categoryService.Add(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromBody] Category category)
        {
            var response = _categoryService.Delete(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] Category category)
        {
            var response = _categoryService.Update(category);

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
