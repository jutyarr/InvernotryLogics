using InvernotryLogics.Logics;
using InvernotryLogics.Models;
using InvernotryLogics.Repositories;
using System;

namespace InvernotryLogics
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductState ss = new ProductState();
            Console.WriteLine($"Unit Cost: {ss.GetProductView(1, 10)}");
            InventoryDbContext context = new InventoryDbContext();
            /*
            var ss = context.Orders.Add(new Order()
            {
                CustomerId = 1,
                OrderedDate = DateTime.UtcNow
            });
            context.SaveChanges();
            */
            #region Products
            ProductRepository prds = new ProductRepository();
            Console.WriteLine("------------Product List----------------");

            foreach (var prd in prds.GetProducts())
            {
                Console.WriteLine($"ID: {prd.Id} \t Name: {prd.Name} \t Unit Price: {prd.UnitPrice}");
            }
            Console.WriteLine("------------End of Product List----------------\n");

            Console.WriteLine("------------Stock List----------------");
            foreach (var stk in prds.GetStocks())
            {
                Console.WriteLine($"ID: {stk.Id} \t ProductID: {stk.ProductId} \t Quanity: {stk.Quantity}" +
                    $"\t Unit Cost: {stk.UnitCost} \t Date: {stk.PurchaseDate.ToShortDateString()} \t Total: {stk.Quantity * stk.UnitCost}");
            }
            Console.WriteLine("------------End of Stock List----------------\n");
            #endregion Products

            #region Orders
            Console.WriteLine("------------Order List----------------");
            OrderRepository ords = new OrderRepository();
           //ords.AddOrderDetail(2, 1, 3);
           // ords.AddOrderDetail(2, 1, 3);

            foreach (var ord in ords.GetOrders())
            {
                Console.WriteLine($"Id: {ord.Id} \t CustomerId: {ord.CustomerId} \t Date: {ord.OrderedDate.ToShortDateString()}");
            }
            Console.WriteLine("------------End Order List----------------\n");
            Console.WriteLine("------------Order Details List----------------");

            foreach (var ord in ords.GetOrderDetails())
            {
                Console.WriteLine($"ID: {ord.Id} \t OrderID: {ord.OrderId} \t Product Id: {ord.ProductId} " +
                    $"\t Quantity: {ord.Quantity} \t Unit Price: {ord.UnitPrice} \t Unit Cost: {ord.UnitCost} " +
                    $"\t Total Price {ord.Quantity * ord.UnitPrice} ");
            }
            Console.WriteLine("------------End Order Details List----------------\n");


            #endregion
            Console.ReadLine();
        }
    }
}
