using Blacksmith.Assignment.Common;
using Blacksmith.Assignment.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blacksmith.Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("onlyBlacksmithOrigins")]
    public class UserController : BaseController
    {
        

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Submits user info
        /// </summary>
        /// <param name="user"></param>   
        [HttpPost]
        [Route("Submit")]
        public Response<User> SubmitUser(User user)
        {
         
            if (ModelState.IsValid)
            {
                return Response<User>.GetResponse(user, "Successfuly Saved");
            }
            else
            {
                HttpContext.Response.StatusCode = Constants.HttpBadRequestCode;
                return Response<User>.GetResponse(Constants.CustomHttpValidationErrorCode, GetModelErros());
            }
        }

       


    }
}
