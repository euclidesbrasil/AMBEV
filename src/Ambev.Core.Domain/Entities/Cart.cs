using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Cart:BaseEntityNoRelational
    {
        // Propriedades
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartItem> Products { get; internal set; } = new List<CartItem>();


        // Construtor privado para garantir que a entidade seja criada através de métodos específicos
        private Cart() { }

        // Construtor público para criação de um novo carrinho
        public Cart(int userId, DateTime date, List<CartItem> products)
        {
            UserId = userId;
            Date = date;
            AddProducts(products);
        }

        public void Update(int userId, DateTime date, List<CartItem> products)
        {
            UserId = userId;
            Date = date;
            AddProducts(products);
        }

        // Método para adicionar produtos ao carrinho
        public void AddProducts(List<CartItem> products)
        {
            if (products == null || !products.Any())
                throw new ArgumentException("A lista de produtos não pode ser vazia.");

            foreach (var product in products)
            {
                var existingProduct = Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    // Se o produto já existe no carrinho, atualiza a quantidade
                    existingProduct.UpdateQuantity(existingProduct.Quantity + product.Quantity);
                }
                else
                {
                    // Caso contrário, adiciona o novo produto ao carrinho
                    Products.Add(product);
                }
            }
        }

        // Método para remover um produto do carrinho
        public void RemoveProduct(int productId)
        {
            var productToRemove = Products.FirstOrDefault(p => p.ProductId == productId);
            if (productToRemove == null)
                throw new ArgumentException("Produto não encontrado no carrinho.");

            Products.Remove(productToRemove);
        }

        public void RemoveAllProducts()
        {
            Products = new List<CartItem>();
        }

        // Método para atualizar a quantidade de um produto no carrinho
        public void UpdateProductQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var productToUpdate = Products.FirstOrDefault(p => p.ProductId == productId);
            if (productToUpdate == null)
                throw new ArgumentException("Produto não encontrado no carrinho.");

            productToUpdate.UpdateQuantity(quantity);
        }

        // Método para calcular o total do carrinho
        public decimal CalculateTotal()
        {
            return Products.Sum(p => p.Quantity * p.Price);
        }
    }
}
