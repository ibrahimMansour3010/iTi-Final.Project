using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class ImportItemsServices
    {
        Context Context;
        public ImportItemsServices()
        {
            Context = new Context();
        }
        public dynamic GetAllImports(int stock_id)
        {
            return (from s in this.Context.Stocks
                      join cs in this.Context.CategoryInStocks
                      on s.ID equals cs.Stock_ID
                      join c in this.Context.Categories
                      on cs.Cat_ID equals c.ID
                      join it in this.Context.Items
                      on c.ID equals it.Cat_ID
                      join im in this.Context.ImportItems
                      on it.ID equals im.Item_ID
                      where s.ID == stock_id
                      select (new
                      {
                          ID = im.ID,
                          ItemID = it.ID,
                          Item = it.Name,
                          Category = c.Name,
                          Quantity = im.Quantity,
                          Date = im.Date
                      })).ToList();
        }
        public void AddImport(ImportItem importItem)
        {
            this.Context.ImportItems.Add(importItem);
            this.Context.SaveChanges();
        }
        public void EditImport(int import_item_id, int quantity)
        {
            this.Context.ImportItems.Where(im => im.ID == import_item_id).First().Quantity = quantity;
            this.Context.SaveChanges();
        }
        public void EditImport(int import_item_id, DateTime date)
        {
            this.Context.ImportItems.Where(im => im.ID == import_item_id).First().Date = date;
            this.Context.SaveChanges();
        }
        public void DeleteImport(int import_item_id)
        {
            ImportItem importItem = this.Context.ImportItems.Where(im => im.ID == import_item_id).First();
            this.Context.ImportItems.Remove(importItem);
            this.Context.SaveChanges();
        }
    }
}
