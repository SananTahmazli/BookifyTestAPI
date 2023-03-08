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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpPost]
        [Route("createauthor")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                await _authorRepository.CreateAsync(authorDTO);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPut]
        [Route("updateauthor")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var author = await _authorRepository.UpdateAsync(authorDTO);
                return Ok(author);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpDelete]
        [Route("deleteauthor/{id}")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            try
            {
                await _authorRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getauthor/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            try
            {
                var author = await _authorRepository.GetAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getallauthors")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authorList = await _authorRepository.GetAllAsync();
                return Ok(authorList);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getauthor/books")]
        [Authorize]
        public IActionResult GetBooksOfAuthor(int authorId)
        {
            try
            {
                var bookList = _authorRepository.GetBooksOfAuthor(authorId);
                return Ok(bookList);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }
    }
}