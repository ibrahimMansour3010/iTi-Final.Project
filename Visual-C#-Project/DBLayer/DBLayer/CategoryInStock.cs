using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class CategoryInStock
    {
        public int ID { get; set; }
        [ForeignKey("Category")]
        public int Cat_ID { get; set; }
        [ForeignKey("Stock")]
        public int Stock_ID { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Category Category { get; set; }

    }
}
