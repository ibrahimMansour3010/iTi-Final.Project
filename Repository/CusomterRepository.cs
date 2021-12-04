﻿using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Customer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace Repository
{
    public class CusomterRepository : ICustomerRepository
    {
        UserManager<CustomerEntity> UserManager;
        SignInManager<CustomerEntity> SignInManager;
        IConfiguration Configuration;
        Result Result;
        public CusomterRepository(UserManager<CustomerEntity> userManager,
            SignInManager<CustomerEntity> signInManager,
            IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
            SignInManager = signInManager;
            Result = new Result();
        }

        public async Task<Result> Signup(SignUpViewModel signUpViewModel)
        {
            CustomerEntity user = signUpViewModel.ToCusomerModel();
            var result = await UserManager.CreateAsync(user, signUpViewModel.Password);
            if (!result.Succeeded)
            {
                Result.IsSuccess = false;
                Result.Data = result.Errors;
                Result.Message = "Cannot Creat Customer";
            }
            else
            {
                Result.IsSuccess = true;
                Result.Data = user;
                Result.Message = "Customer Has Been Created Successfully";
                // login
                await SignInManager.PasswordSignInAsync(user,
                    signUpViewModel.Password, false, false);

            }

            return Result;
        }
        public async Task<Result> Login(LoginViewModel loginViewModel)
        {
            // get user who has the login view model
            var user = await UserManager.FindByNameAsync(loginViewModel.Username);
            var result =
                await SignInManager.PasswordSignInAsync(loginViewModel.Username
                , loginViewModel.Password, loginViewModel.IsRemembered, true);
            if (!result.Succeeded || !await UserManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                Result.IsSuccess = false;
                Result.Data = "";
                Result.Message = "Cannot Login ";
            }
            else
            {
                // create claims 
                var claims = new List<Claim>(){
                new Claim("UserID" , user.Id),
                };
                // create key
                var key =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
                // create token
                var Token = new JwtSecurityToken
                    (
                        issuer: Configuration["JWT:ValidIssuer"],
                        audience: Configuration["JWT:ValidAudiene"],
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials:
                        new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                        claims: claims
                    );
                Result.IsSuccess = true;
                // return token
                Result.Data = new JwtSecurityTokenHandler().WriteToken(Token);
                Result.Message = "Login Successfully ";
            }

            return Result;
        }
        public async Task<Result> GetCustomerData(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                Result.IsSuccess = false;
                Result.Data = "";
                Result.Message = "Cannot Find Customer With This ID";
            }
            else
            {
                Result.IsSuccess = true;
                Result.Data = await UserManager.FindByIdAsync(id);
                Result.Message = "User Data Retrieved Successfully";
            }
            return Result;
        }
        public async Task<Result> EditProfile(EditCusomerViewModel editCusomerViewModel)
        {
            var user = await UserManager.FindByIdAsync(editCusomerViewModel.Id);
            if (user == null)
            {
                Result.IsSuccess = false;
                Result.Data = "";
                Result.Message = "Cannot Find Customer With This ID";
            }
            else
            {
                user.Image = editCusomerViewModel.Image ?? "";
                user.Firstname = editCusomerViewModel.Firstname;
                user.Lastname = editCusomerViewModel.Lastname;
                user.Email = editCusomerViewModel.Email;
                user.Gender = editCusomerViewModel.Gender;
                user.UserName = editCusomerViewModel.UserName;
                user.PhoneNumber = editCusomerViewModel.PhoneNumeber;
                user.Address = editCusomerViewModel.Address;

                var res = await UserManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    Result.IsSuccess = true;
                    Result.Data = user;
                    Result.Message = "User Data Has Been Updated Successfully";
                }
                else
                {
                    Result.IsSuccess = false;
                    Result.Data = res.Errors;
                    Result.Message = "User Data Has Not Been Updated Successfully";
                }
            }

            return Result;
        }

        public async Task<Result> ForegetPassword(string Email)
        {
            var user = await UserManager.FindByEmailAsync(Email);
            if (user != null)
            {
                Result.IsSuccess = true;
                Result.Data = await UserManager.GeneratePasswordResetTokenAsync(user);
                Result.Message = "Password Reset Token Has Been Generated Successfully";
            }
            else
            {
                Result.IsSuccess = false;
                Result.Data = "";
                Result.Message = "Cannot Find User With This Email";
            }
            return Result;
        }
        public async Task<Result> RestPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await UserManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user != null)
            {
                var result =
                    await UserManager.ResetPasswordAsync(user, resetPasswordViewModel.Token,
                                                        resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    Result.IsSuccess = true;
                    Result.Data = "";
                    Result.Message = "Password Rest Successfully";
                }
                else
                {
                    Result.IsSuccess = false;
                    Result.Data = result.Errors;
                    Result.Message = "Cannot Reset Password";
                }
                return Result;
            }
            else
            {
                Result.IsSuccess = false;
                Result.Data = "";
                Result.Message = "Cannot Find User With This Email";
            }
            return Result;
        }
    }
}
