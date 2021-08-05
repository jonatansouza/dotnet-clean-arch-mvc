﻿using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers {
    class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product> {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository) {
            _repository = repository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
