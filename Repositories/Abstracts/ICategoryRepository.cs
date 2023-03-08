using DataAccess.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstracts
{
    public interface ICategoryRepository : IGenericRepository<CategoryDTO, Category, CategoryDTO>
    {
        IEnumerable<BookDTO> GetAllBooksInCategory(int categoryId);
    }
}