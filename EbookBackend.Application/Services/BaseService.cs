using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            if (model == null) return false;

            await _repository.DeleteAsync(model);
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            return entity;
        }
    }
}
