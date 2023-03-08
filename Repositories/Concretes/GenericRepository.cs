using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class GenericRepository<TRequestDTO, TEntity, TResponseDTO>
        : IGenericRepository<TRequestDTO, TEntity, TResponseDTO> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _mapper = mapper;
        }

        public virtual async Task<TResponseDTO> CreateAsync(TRequestDTO requestDTO)
        {
            var entity = _mapper.Map<TRequestDTO, TEntity>(requestDTO);
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }

        public virtual async Task<TResponseDTO> UpdateAsync(TRequestDTO requestDTO)
        {
            var entity = _mapper.Map<TRequestDTO, TEntity>(requestDTO);
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
        {
            var entityList = await _dbSet.ToListAsync();
            var responseDTO = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TResponseDTO>>(entityList);
            return responseDTO;
        }

        public virtual async Task<TResponseDTO> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }
    }
}