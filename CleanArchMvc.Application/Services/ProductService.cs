using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services {
    class ProductService : IProductService {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(ProductDTO product) {
            var entity = _mapper.Map<Product>(product);
            await _repository.CreateAsync(entity);
        }

        public async Task<ProductDTO> GetById(int? id) {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(entity);
        }
        public async Task<ProductDTO> GetProductCategory(int? id) {
            var entity = await _repository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDTO>(entity);
        }
        public async Task<IEnumerable<ProductDTO>> GetProducts() {
            var entity = await _repository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(entity);
        }

        public async Task Remove(int? id) {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(entity);
        }

        public async Task Update(ProductDTO category) {
            var entity = _mapper.Map<Product>(category);
            await _repository.UpdateAsync(entity);
        }
    }
}
