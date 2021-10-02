using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class ExportItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Item")]
        public int Item_ID { get; set; }
        public virtual Item Item { get; set; }
    }
}
