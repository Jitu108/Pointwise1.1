using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.Attributes;
using Pointwise.Common.DTO;
using Pointwise.Domain.Models;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.API.Admin.Controllers
{
    [Authorize]
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

            this.loggedInUserId = nameClaim != null? Int32.Parse(nameClaim.Value) : 0;
        }

        [HttpGet]
        [CustomAuthorize()]
        public IActionResult Get()
        {
            try
            {
                var entities = userService.GetUsers();
                var entitiesdto = entities
                    .Select(x => mapper.Map<UserDto>(x))
                    .ToList();

                if (entitiesdto.Any()) return Ok(entitiesdto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetUserById")]
        //[CustomAuthorize()]
        public IActionResult GetById(int id)
        {
            try
            {
                var entitydto = mapper.Map<UserDto>(userService.GetById(id));

                if (entitydto != null) return Ok(entitydto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody]UserDto user)
        {
            try
            {
                if (!ModelState.IsValid || user == null) return BadRequest(ModelState);

                var domainEntity = mapper.Map<User>(user);
                //domainEntity.CreatedBy = loggedInUserId;

                var addedEntity = userService.Add(domainEntity);
                if (addedEntity == null)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving the User {user.FirstName + " " + user.LastName}");
                    return StatusCode(500, ModelState);
                }
                var addedEntityDto = mapper.Map<UserDto>(addedEntity);
                return CreatedAtRoute("GetTagById", new { id = addedEntityDto.Id }, addedEntityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [CustomAuthorize()]
        public IActionResult Update(int id, [FromBody]UserDto user)
        {
            try
            {
                if (!ModelState.IsValid || user == null) return BadRequest(ModelState);

                var domainEntity = mapper.Map<User>(user);
                domainEntity.CreatedBy = loggedInUserId;
                var updatedEntity = userService.Update(domainEntity);

                if (updatedEntity != null) return Ok(mapper.Map<UserDto>(updatedEntity));
                else
                {
                    ModelState.AddModelError("", $"Something went wrong while updating the user {user.FirstName + " " + user.LastName}");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id:int}")]
        [CustomAuthorize()]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                var status = userService.SoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize()]
        public IActionResult UndoSoftDelete(int id)
        {
            try
            {
                var status = userService.UndoSoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize()]
        public IActionResult Block(int id)
        {
            try
            {
                var status = userService.Block(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize()]
        public IActionResult Unblock(int id)
        {
            try
            {
                var status = userService.Unblock(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}