using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvernotryLogics.Repositories
{
    public class UnitCost
    {
        InventoryDbContext context = new InventoryDbContext();
        public decimal GetUnitCost(int prdId)
        {
            #region Calculate total stock
            var listStock =context.Stocks
                .Where(x => x.ProductId == prdId)
                .Select(x => new StockInvoice
                {
                    ID = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitCost = x.UnitCost
                }).ToList();
            #endregion

            #region Calculate Total Product sold
            var listOrder = context.OrderDetails
                .Where(x => x.ProductId == prdId)
                .Select(x => new
                {
                    ID = x.Id,
                    ProductID = x.ProductId,
                    Qty = x.Quantity,
                    UnitCost = x.UnitCost
                }).ToList();
            #endregion
            var totalSold = listOrder.Sum(x => x.Qty);
            decimal cost = 0;

            while (totalSold > 0)
            {
                foreach (var stk in listStock)
                {
                    if (totalSold - stk.Quantity > 0)
                    {
                        totalSold -= stk.Quantity;
                    }
                    //ex 0.5 left of totalSold

                    else
                    {
                        int getStockId = stk.ID;
                        var ss = listStock.Where(s => s.ID == stk.ID).FirstOrDefault();
                        ss.Quantity -= totalSold;

                        for (int i = 1; i <= (int)stk.Quantity - (int)totalSold; i++)
                        {
                            if (totalSold < 1)
                                break;

                            totalSold -= 1;

                        }
                        cost = stk.UnitCost;
                        totalSold = 0;
                        break;

                    }
                }
            }

            return cost;
        }

        public class StockInvoice
        {
            public int ID { get; set; }
            public int ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitCost { get; set; }
        }

    }
}
