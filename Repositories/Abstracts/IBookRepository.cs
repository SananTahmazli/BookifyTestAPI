using DataAccess.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstracts
{
    public interface IBookRepository : IGenericRepository<BookDTO, Book, BookDTO>
    {
        AuthorDTO GetAuthorOfBook(int bookId);
        CategoryDTO GetCategoryOfBook(int bookId);
    }
}