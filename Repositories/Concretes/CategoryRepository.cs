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
    public class CategoryRepository : GenericRepository<CategoryDTO, Category, CategoryDTO>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<BookDTO> GetAllBooksInCategory(int categoryId)
        {
            var bookList = _dbContext.Books.Where(b => b.CategoryId == categoryId).ToList();
            var bookDTOList = _mapper.Map<IEnumerable<BookDTO>>(bookList);
            return bookDTOList;
        }
    }
}