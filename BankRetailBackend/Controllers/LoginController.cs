using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankRetailBackend.DBModels;
using BankRetailBackend.Managers;
using BankRetailBackend.Repository;
using BankRetailBackend.SessionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankRetailBackend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserstoreRepository _userstoreRepository;

        public LoginController(IUserstoreRepository userstoreRepository)
        {
            this._userstoreRepository = userstoreRepository;
        }

        public class LoginModel
        {
            public string LoginId { get; set; }
            public string Pw { get; set; }
        }

        [HttpPost]
        public IActionResult login([FromBody] LoginModel user)
        {
            if (user.LoginId == null || user.Pw == null)
            {
                return BadRequest();
            }

            Userstore userInfo = _userstoreRepository.getUser(user.LoginId, user.Pw);
            if (userInfo == null)
            {
                return Unauthorized();
            }

            IAuthContainerModel model = JWTContainerModel.GetJWTContainerModel(user.LoginId, user.Pw, userInfo.RoleId);
            JWTService service = new JWTService("Vfru3PuNevbsh5Cb2J4nh2sjM928P7pd");
            string token = service.generateToken(model);
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("token", token);
            response.Add("roleId", Convert.ToString(userInfo.RoleId));

            return Ok(response);
        }
    }
}