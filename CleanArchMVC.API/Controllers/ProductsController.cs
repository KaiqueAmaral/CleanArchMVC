﻿using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMVC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productService.GetProducts();

        if (products == null)
        {
            return NotFound("Products not found");
        }

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var products = await _productService.GetById(id);

        if (products == null)
        {
            return NotFound("Product not found");
        }

        return Ok(products);
    }


    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Invalid Data");
        }

        await _productService.Add(productDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest();
        }

        if (productDto == null)
        {
            return BadRequest();
        }

        await _productService.Update(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var product = await _productService.GetById(id);

        if (product == null)
        {
            return NotFound("Product not found");
        }

        await _productService.Remove(id);

        return Ok(product);
    }
}
