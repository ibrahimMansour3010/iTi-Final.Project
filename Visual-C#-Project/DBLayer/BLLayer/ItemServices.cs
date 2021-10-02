using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class ItemServices
    {
        Context Context;
        public ItemServices()
        {
            Context = new Context();
        }
        public void AddItem(Item item)
        {
            this.Context.Items.Add(item);
            this.Context.SaveChanges();
        }
        public void EditItem(int item_id, string name)
        {
            this.Context.Items.Where(i => i.ID == item_id).First().Name = name;
            this.Context.SaveChanges();
        }
        public void EditItem(int item_id, int cat_id)
        {
            this.Context.Items.Where(i => i.ID == item_id).First().Cat_ID = cat_id;
            this.Context.SaveChanges();
        }
        public void UpdateQuantity(int item_id, int quantity)
        {
            this.Context.Items.Where(i => i.ID == item_id).First().Quantity += quantity;
            this.Context.SaveChanges();
        }
        public void DeleteItem(int item_id)
        {
            Item item = this.Context.Items.Where(i => i.ID == item_id).First();
            this.Context.Items.Remove(item);
            this.Context.SaveChanges();
        }

        public List<Item> GetAllItems(int? cat_id)
        {
            return this.Context.Items.Where(i=>i.Cat_ID == cat_id).ToList();
        }

        public int GetCatID(int item_id)
        {
            return this.Context.Items.Where(i => i.ID == item_id).First().Cat_ID;
        }

        public int GetQuantity(int? item_id)
        {
            if (item_id == null)
                return 0;
            return this.Context.Items.Where(i => i.ID == item_id).First().Quantity;
        }

        public List<Item> GetReoprt(int stock_id)
        {
            return (from s in this.Context.Stocks
                    join cs in this.Context.CategoryInStocks
                    on s.ID equals cs.Stock_ID
                    join c in this.Context.Categories
                    on cs.Cat_ID equals c.ID
                    join it in this.Context.Items
                    on c.ID equals it.Cat_ID
                    where s.ID == stock_id
                    select it).ToList();
        }
    }
}
