using DTOs;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstracts;
using Repositories.Concretes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Route("createcategory")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                await _categoryRepository.CreateAsync(categoryDTO);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPut]
        [Route("updatecategory")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _categoryRepository.UpdateAsync(categoryDTO);
                return Ok(category);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getcategory/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getallcategoriess")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categoryList = await _categoryRepository.GetAllAsync();
                return Ok(categoryList);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getcategory/books")]
        public IActionResult GetBooksOfCategory(int categoryId)
        {
            try
            {
                var bookList = _categoryRepository.GetAllBooksInCategory(categoryId);
                return Ok(bookList);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }
    }
}