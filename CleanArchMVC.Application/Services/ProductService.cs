﻿using AutoMapper;
using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Application.Products.Commands;
using CleanArchMVC.Application.Products.Queries;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery == null) 
            throw new Exception(nameof(productsQuery));

        var result =  await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetById(int? id)
    {
        var productQuery = new GetProductByIdQuery(id.Value);

        if (productQuery == null)
            throw new Exception(nameof(productQuery));

        var result = await _mediator.Send(productQuery);

        return _mapper.Map<ProductDTO>(result);

    }

    public async Task<ProductDTO> GetProductCategory(int? id)
    {
        var productQuery = new GetProductByIdQuery(id.Value);

        if (productQuery == null)
            throw new Exception(nameof(productQuery));

        var result = await _mediator.Send(productQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    public async Task Add(ProductDTO productDto)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDto)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id.Value);

        if (productRemoveCommand == null)
            throw new Exception("Entity could not be loaded");

        await _mediator.Send(productRemoveCommand);
    }
}
