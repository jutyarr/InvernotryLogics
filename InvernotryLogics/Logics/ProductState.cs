using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvernotryLogics.Logics
{
    public class ProductState
    {
        InventoryDbContext context = new InventoryDbContext();

        public decimal GetProductView(int id, decimal qty)
        {
            decimal currentOrd = qty;

            var prds = context.Products.Find(id);
            var det = context.OrderDetails.Where(d => d.ProductId == id).ToList();
            decimal totalSold = det.Sum(s => s.Quantity);

            var stock = context.Stocks.Where(s => s.ProductId == id).ToList();

            List<StokTemp> stokTemps = new List<StokTemp>();
            foreach (var item in stock)
            {
                stokTemps.Add(new StokTemp
                {
                    Id = item.Id,
                    Qty = item.Quantity,
                    UnitCost = item.UnitCost
                });
            }
            List<CalculatePrice> prices = new List<CalculatePrice>();
            foreach (var item in stokTemps)
            {
                //we each if totalSold - itemQty is greater than 0
                //if so we we decrease totalQty by itemQty
                //we will not calculate unit cost here, we try to decrease totalSold to zero
                if (totalSold - item.Qty > 0)
                {
                    totalSold -= item.Qty;
                }
                //maybe TotalSold is 0 but for current item there are still qty left
                if (totalSold - item.Qty < 0)
                {
                    //item Qty is bigger than totalSold, so first we decrease item.Qty
                    //by the amount left in totalSold, then totalSold should be 0
                    //even if the loop contioue the item.Qty will be decreased by 0 which doesn't affect it
                    item.Qty -= totalSold;
                    totalSold = 0;

                    //now we check if avaliable item.Qty is greater then else let the loop go to the next item

                    //FROM HERE WE TRY TO GET THE UNIT COSTING
                    if (item.Qty > 0)
                    {
                        //enough qty is avaliable so we can use this item as costing unit,
                        //if so add this to CalculatePrice List
                        if(item.Qty - currentOrd >= 0)
                        {
                            prices.Add(new CalculatePrice()
                            {
                                Qty = currentOrd,
                                UnitCost = item.UnitCost,
                                TotalCost = currentOrd * item.UnitCost
                            });
                            //If we got here that mean our job is done so break the loop
                            //we done for currentOrd we can stop the loop
                            break;
                        }
                        //our order is greater than than the qty amount let the loop continoue 
                        else
                        {
                            // we all all avalilabe item.qty and then de decrease currentOrd by item.Qty
                            currentOrd -= item.Qty;
                            //we add item.Qty to price list
                            prices.Add(new CalculatePrice()
                            {
                                Qty = item.Qty,
                                UnitCost = item.UnitCost,
                                TotalCost = item.Qty * item.UnitCost

                            });
                        }
                        //in any case currentOrd == 0 we should break
                        if (currentOrd == 0)
                            break;
                    }
                   
                }
               
            }

            //Get Qties
            decimal totalQties = prices.Sum(q => q.Qty);
            //Get Total Costs
            decimal totalCost = prices.Sum(c => c.TotalCost);
            //Get Cost per Unit
            decimal costPerUnit = totalCost / totalQties;
            return costPerUnit;

        }

    }
    public class StokTemp
    {
        public int Id { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
    }
    public class CalculatePrice
    {
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
