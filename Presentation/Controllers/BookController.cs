using DTOs;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstracts;
using System;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        [Route("createbook")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO)
        {
            try
            {
                await _bookRepository.CreateAsync(bookDTO);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPut]
        [Route("updatebook")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> UpdateBook([FromBody] BookDTO bookDTO)
        {
            try
            {
                var book = await _bookRepository.UpdateAsync(bookDTO);
                return Ok(book);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpDelete]
        [Route("deletebook/{id}")]
        [Authorize(Roles = RoleKeywords.AdminRole)]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getbook/{id}")]
        [Authorize]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            try
            {
                var book = await _bookRepository.GetAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getallbooks")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var bookList = await _bookRepository.GetAllAsync();
                return Ok(bookList);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getauthorofbook")]
        [Authorize]
        public IActionResult GetAuthorOfBook(int bookId)
        {
            try
            {
                var author = _bookRepository.GetAuthorOfBook(bookId);
                return Ok(author);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("getcategoryofbook")]
        [Authorize]
        public IActionResult GetCategoryOfBook(int bookId)
        {
            try
            {
                var author = _bookRepository.GetCategoryOfBook(bookId);
                return Ok(author);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }
    }
}