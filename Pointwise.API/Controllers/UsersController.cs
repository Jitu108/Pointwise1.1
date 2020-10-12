using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private int loggedInUserId;
        public UsersController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            var nameClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);

            this.loggedInUserId = nameClaim != null ? Int32.Parse(nameClaim.Value) : 0;
        }

        [HttpGet("{id:int}", Name = "GetUserById")]
        //[CustomAuthorize()]
        public IActionResult GetById(int id)
        {
            try
            {
                //var entitydto = mapper.Map<UserDto>(userService.GetById(id));

                //if (entitydto != null) return Ok(entitydto);
                //else 
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
