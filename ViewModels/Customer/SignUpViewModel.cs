﻿using Models.Cart;
using Models.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Customer
{
    public class SignUpViewModel
    {
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }

    }
    public static class AddCustomerExtensions
    {
        public static CustomerEntity ToCusomerModel(this SignUpViewModel addCustomer)
        {
            CustomerEntity customerEntity =  new CustomerEntity()
            {
                Firstname = addCustomer.Firstname,
                Lastname = addCustomer.Lastname,
                Email = addCustomer.Email,
                UserName = addCustomer.Username,
                Address = addCustomer.Address,
                PhoneNumber = addCustomer.Phone,
                Gender = addCustomer.Gender,
                Image = addCustomer.Image,
                NormalizedUserName="Customer"
            };
            return customerEntity;
        }

    }
}
