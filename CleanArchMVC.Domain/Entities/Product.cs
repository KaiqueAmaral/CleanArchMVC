using CleanArchMVC.Domain.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace CleanArchMVC.Domain.Entities;

public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; }


    public Product(string name, string description, decimal price, int stock, string image)
    {
        ValidateDomain(name, description, price, stock, image);
    }

    public Product(int id, string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(id < 0, "Invalid id value.");
        Id = id;
        ValidateDomain(name, description, price, stock, image);
    }
    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        CategoryId = categoryId;
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Name is required");
        DomainExceptionValidation.When(description.Length < 5, "Invalid description, too short, minimum 3 characters");

        DomainExceptionValidation.When(price < 0, "Invalid price value.");
        DomainExceptionValidation.When(stock < 0, "Invalid stock value.");

        DomainExceptionValidation.When(image.Length > 250, "Invalid image name, too long, maximum 250 characters");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
    }

    //Propriedades de navegação
    //Um produto está relacionado com uma categoria
    //Mais para frente para o entity framework irá criar as tabelas
    //Ele sabe que isso é uma propriedade de navegação se o tipo que ela ta apontando nao puder ser mapeado
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}
