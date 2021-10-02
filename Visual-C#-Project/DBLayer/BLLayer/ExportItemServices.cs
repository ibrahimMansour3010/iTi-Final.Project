using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class ExportItemServices
    {
        Context Context;
        public ExportItemServices()
        {
            Context = new Context();
        }
        public dynamic GetAllIExports(int stock_id)
        {
            return (from s in this.Context.Stocks
                    join cs in this.Context.CategoryInStocks
                    on s.ID equals cs.Stock_ID
                    join c in this.Context.Categories
                    on cs.Cat_ID equals c.ID
                    join it in this.Context.Items
                    on c.ID equals it.Cat_ID
                    join ex in this.Context.ExportItems
                    on it.ID equals ex.Item_ID
                    where s.ID == stock_id
                    select (new
                    {
                        ID = ex.ID,
                        ItemID = it.ID,
                        Item = it.Name,
                        Category = c.Name,
                        Quantity = ex.Quantity,
                        Date = ex.Date
                    })).ToList();
        }
        public void AddExport(ExportItem exportItem)
        {
            this.Context.ExportItems.Add(exportItem);
            this.Context.SaveChanges();
        }
        public void EditExport(int export_item_id, int quantity)
        {
            this.Context.ExportItems.Where(ex => ex.ID == export_item_id).First().Quantity = quantity;
            this.Context.SaveChanges();
        }
        public void EditExport(int export_item_id, DateTime date)
        {
            this.Context.ExportItems.Where(ex => ex.ID == export_item_id).First().Date = date;
            this.Context.SaveChanges();
        }
        public void DeleteExport(int export_item_id)
        {
            ExportItem exportItem = this.Context.ExportItems.Where(ex => ex.ID == export_item_id).First();
            this.Context.ExportItems.Remove(exportItem);
            this.Context.SaveChanges();
        }
    }
}
