using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class StockServices
    {
        Context Context;
        public StockServices()
        {
            Context = new Context();
        }
        public void AddStock(Stock stock)
        {
            this.Context.Stocks.Add(stock);
            this.Context.SaveChanges();
        }
        public void EditStock(int stock_id,string name)
        {
            this.Context.Stocks.Where(s=>s.ID == stock_id).First().Name = name;
            this.Context.SaveChanges();
        }
        public void DeleteStock(int stock_id)
        {
            Stock stock = this.Context.Stocks.Where(s=>s.ID == stock_id).First();
            this.Context.Stocks.Remove(stock);
            this.Context.SaveChanges();
        }
        public List<Stock> GetStocks()
        {
            return this.Context.Stocks.ToList();
        }
        public int GetStockIdFromCatId(int cat_id)
        {
            int stock_id = (from c in this.Context.Categories
                            join cs in this.Context.CategoryInStocks
                            on c.ID equals cs.Cat_ID
                            where c.ID == cat_id
                            select cs.Stock_ID).First();
            return stock_id;
        }
    }
}
