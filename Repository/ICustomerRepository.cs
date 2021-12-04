﻿using Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace Repository
{
    public interface ICustomerRepository
    {
        Task<Result> Signup(SignUpViewModel signUpViewModel);
        Task<Result> Login(LoginViewModel loginViewModel);
        Task<Result> GetCustomerData(string id);
        Task<Result> EditProfile(EditCusomerViewModel signUpViewModel);
        Task<Result> ForegetPassword(string Email);
        Task<Result> RestPassword(ResetPasswordViewModel resetPasswordViewModel);
    }
}
