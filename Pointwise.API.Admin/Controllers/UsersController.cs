using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.DTO;
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
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id:int}", Name = "GetUserById")]
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
        public IActionResult Create([FromBody]UserDto user)
        {
            try
            {
                if (!ModelState.IsValid || user == null) return BadRequest(ModelState);
                

                var addedEntity = userService.Add(mapper.Map<User>(user));
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
        public IActionResult Update(int id, [FromBody]UserDto user)
        {
            try
            {
                if (!ModelState.IsValid || user == null) return BadRequest(ModelState);

                user.Id = id;
                var updatedEntity = userService.Update(mapper.Map<User>(user));

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

    }
}