using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services {
    public class ProductService : IProductService {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task Add(ProductDTO product) {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);
            await _mediator.Send(productCreateCommand);
        }

        public async Task<ProductDTO> GetById(int? id) {
            var productsQuery = new GetProductByIdQuery(id.Value);
            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<ProductDTO>(result);
        }
        public async Task<IEnumerable<ProductDTO>> GetProducts() {
            var productsQuery = new GetProductsQuery();
            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? id) {
            var command = new ProductRemoveCommand(id.Value);
            await _mediator.Send(command);
        }

        public async Task Update(ProductDTO product) {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(product);
            await _mediator.Send(productUpdateCommand);
        }
    }
}
