using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class BookRepository : GenericRepository<BookDTO, Book, BookDTO>, IBookRepository
    {
        public BookRepository(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public AuthorDTO GetAuthorOfBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            var author = _dbContext.Authors.Where(a => a.Id == book.AuthorId).FirstOrDefault();
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }

        public CategoryDTO GetCategoryOfBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            var category = _dbContext.Categories.Where(c => c.Id == book.CategoryId).FirstOrDefault();
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }
    }
}