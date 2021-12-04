using Microsoft.AspNetCore.Identity;
using Models.Admin;
using Models.Cart;
using Models.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Customer
{
    [Flags]
    public enum Gender
    {
        Male,
        Female
    }
    public class CustomerEntity : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }


        public virtual IEnumerable<CartItemEntity> CartItemEntities { get; set; }
        public CartEntity CartEntity { get; set; }
    }
}
