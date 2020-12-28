using InvernotryLogics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvernotryLogics.Repositories
{
    public class OrderRepository
    {
        InventoryDbContext context = new InventoryDbContext();
        public List<Order> GetOrders()
        {
             var orders = context.Orders.ToList();
                return orders;
        }
        public List<OrderDetail> GetOrderDetails()
        {
                var orderDetails = context.OrderDetails.ToList();
                return orderDetails;
        }


        public void AddOrderDetail(int ordId, int prdId, decimal qty)
        {
            UnitCost unitCost = new UnitCost();
            decimal cost = unitCost.GetUnitCost(prdId);
            
            context.OrderDetails.Add(new OrderDetail()
            {
                OrderId = ordId,
                ProductId = prdId,
                Quantity = qty,
                UnitPrice = 2,
                UnitCost = cost
            });
            context.SaveChanges();
        }
        /*Hardcoded
        public List<Order> GetOrders = new List<Order>()
        {
            new Order(){ Id = 1, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(-5)},
            new Order(){ Id = 2, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(-4)},
            new Order(){ Id = 3, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(-3)},
            new Order(){ Id = 4, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(-2)},
            new Order(){ Id = 5, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(-1)},
            new Order(){ Id = 6, CustomerId = 1, OrderedDate = DateTime.UtcNow.AddDays(0)}
        };

        public List<OrderDetail> GetOrderDetails = new List<OrderDetail>()
        {
            new OrderDetail() {Id = 1, OrderId = 1, ProductId = 1, Quantity = 9, UnitPrice = 10, UnitCost = 10 },
            new OrderDetail() {Id = 2, OrderId = 1, ProductId = 2, Quantity = 13, UnitPrice = 10, UnitCost = 10 },
            new OrderDetail() {Id = 3, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 10, UnitCost = 10 },
            new OrderDetail() {Id = 4, OrderId = 1, ProductId = 4, Quantity = 2, UnitPrice = 10, UnitCost = 10 },
            new OrderDetail() {Id = 5, OrderId = 1, ProductId = 5, Quantity = 23, UnitPrice = 10, UnitCost = 10 },
        };
        */

    }
}
