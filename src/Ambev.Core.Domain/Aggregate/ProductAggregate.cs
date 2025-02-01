using Ambev.Core.Domain.Common;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ambev.Core.Domain.Aggregate;


using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.ValueObjects;

public sealed class ProductAggregate
{
    public Product Product { get; private set; }

    public ProductAggregate(Product product)
    {
        Product = product;
    }

    public void UpdateProduct(string title, decimal price, string description,
                              string category, string image, Rating rating)
    {
        Product.Update(title, price, description, category, image, rating);
    }
}