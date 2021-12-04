using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Customer;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        ICustomerRepository CustomerRepository;
        
        public ProfileController(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }
        [HttpGet("MyProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            string id = User.Claims.FirstOrDefault(i => i.Type == "UserID").Value;
            var user = await CustomerRepository.GetCustomerData(id);
            return Ok( user );
            
        }
        [HttpPut("Edit")]
        [Authorize]
        public async Task<IActionResult> EditUserProfile([FromBody]EditCusomerViewModel editCusomerViewModel)
        {
            editCusomerViewModel.Id = User.Claims.FirstOrDefault(i => i.Type == "UserID").Value;
            var user = await CustomerRepository.EditProfile(editCusomerViewModel);
            if (user == null)
                return Ok("Empty");
            return Ok(user);
        }
    }
}
