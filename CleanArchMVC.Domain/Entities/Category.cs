﻿using CleanArchMVC.Domain.Validation;
using System.Runtime.CompilerServices;

namespace CleanArchMVC.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }
    public ICollection<Product> Products { get; set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }
     
    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid id value.");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

        Name = name;
    }
}
