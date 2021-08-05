using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services {
    class CategoryService : ICategoryService {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(CategoryDTO category) {
            var entity = _mapper.Map<Category>(category);
            await _repository.CreateAsync(entity);
        }

        public async Task<CategoryDTO> GetById(int? id) {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories() {
            var entity = await _repository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(entity);
        }

        public async Task Remove(int? id) {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(entity);
        }

        public async Task Update(CategoryDTO category) {
            var entity = _mapper.Map<Category>(category);
            await _repository.UpdateAsync(entity);
        }
    }
}
