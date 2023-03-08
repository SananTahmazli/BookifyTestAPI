using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities;
using DTOs;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class AuthorRepository : GenericRepository<AuthorDTO, Author, AuthorDTO>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<BookDTO> GetBooksOfAuthor(int authorId)
        {
            var bookList = _dbContext.Books.Where(b => b.AuthorId == authorId).ToList();
            var bookDTOList = _mapper.Map<IEnumerable<BookDTO>>(bookList);
            return bookDTOList;
        }
    }
}