using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class CategoryServices
    {
        Context Context;
        public CategoryServices()
        {
            Context = new Context();
        }
        public List<Category> GetAllCategories()
        {
            return this.Context.Categories.ToList();
        }
        public List<Category> GetAllCategories(int stock_id)
        {
            List < Category > categories = (from c in this.Context.Categories
                                 join cs in this.Context.CategoryInStocks
                                 on c.ID equals cs.Cat_ID
                                 where cs.Stock_ID == stock_id
                                 select c).ToList();
            return categories;
        }
        public void AddCategory(Category category,int stock_id)
        {
            this.Context.Categories.Add(category);
            this.Context.CategoryInStocks.Add(new CategoryInStock { Cat_ID = category.ID, Stock_ID = stock_id });
            this.Context.SaveChanges();
        }
        public void EditCategory(int cat_id, string name)
        {
            this.Context.Categories.Where(c => c.ID == cat_id).First().Name = name;
            this.Context.SaveChanges();
        }
        public void EditCategory(int cat_id, int stock_id)
        {
            this.Context.CategoryInStocks.Where(c => c.Cat_ID == cat_id).First().Stock_ID = stock_id;
            this.Context.SaveChanges();
        }
        public void DeleteCategory(int cat_id)
        {
            Category category  = this.Context.Categories.Where(c => c.ID == cat_id).First();
            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
        }

    }
}
