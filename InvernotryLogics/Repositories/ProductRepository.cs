using InvernotryLogics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvernotryLogics.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts()
        {
            using var context = new InventoryDbContext();
            var prds = context.Products.ToList();
            return prds;
        }


        public List<Stock> GetStocks()
        {
            using (var context = new InventoryDbContext())
            {
                var stocks = context.Stocks.ToList();
                return stocks;

            }
        }
        /*Hardcoded list
        public List<Product> GetProduct = new List<Product>()
        {
            new Product() { Id = 1, Name = "First Product", UnitPrice = 10},
            new Product() { Id = 2, Name = "Second Product", UnitPrice = 20},
            new Product() { Id = 3, Name = "Third Product", UnitPrice = 30},
            new Product() { Id = 4, Name = "Fourth Product", UnitPrice = 40},
            new Product() { Id = 5, Name = "Fifth Product", UnitPrice = 50},

        };

        public List<Stock> GetStocks = new List<Stock>()
        {
            new Stock() {Id = 1, ProductId = 1 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},
            new Stock() {Id = 2, ProductId = 1 , Quantity = 10, UnitCost = 10,
                PurchaseDate = DateTime.UtcNow.AddDays(-9)},
            new Stock() {Id = 3, ProductId = 2 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},
            new Stock() {Id = 4, ProductId = 2 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-9)},
            new Stock() {Id = 5, ProductId = 3 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},
            new Stock() {Id = 6, ProductId = 3 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},

            new Stock() {Id = 7, ProductId = 4 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},
            new Stock() {Id = 8, ProductId = 4 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-9)},

            new Stock() {Id = 9, ProductId = 5, Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-10)},
            new Stock() {Id = 10, ProductId = 5 , Quantity = 10, UnitCost = 5,
                PurchaseDate = DateTime.UtcNow.AddDays(-9)}
        };
        */
    }
}
