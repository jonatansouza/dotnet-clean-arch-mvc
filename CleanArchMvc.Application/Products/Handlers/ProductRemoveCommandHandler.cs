using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers {
    class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product> {
        private readonly IProductRepository _repository;

        public ProductRemoveCommandHandler(IProductRepository repository) {
            _repository = repository;
        }
        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken) {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null) {
                throw new ApplicationException($"Error Creating entity");
            }

            return await _repository.RemoveAsync(product);
        }
    }
}
