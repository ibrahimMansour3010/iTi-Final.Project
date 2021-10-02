using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<CategoryInStock> CategoryInStocks { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
