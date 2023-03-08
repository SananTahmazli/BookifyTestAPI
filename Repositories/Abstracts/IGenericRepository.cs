using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstracts
{
    public interface IGenericRepository<TRequestDTO, TEntity, TResponseDTO>
    {
        Task<TResponseDTO> CreateAsync(TRequestDTO requestDTO);
        Task<TResponseDTO> UpdateAsync(TRequestDTO requestDTO);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<TResponseDTO>> GetAllAsync();
        Task<TResponseDTO> GetAsync(int id);
    }
}