using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.Attributes;
using Pointwise.Domain.Enums;

namespace Pointwise.API.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticDataController : ControllerBase
    {
        [HttpGet("[action]")]
        [CustomAuthorize()]
        public IActionResult Entities()
        {
            try
            {
               var entities = Enum.GetValues(typeof(EntityType))
                    .Cast<EntityType>()
                    .Select(v => v.ToString())
                    .ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        [CustomAuthorize()]
        public IActionResult AccessTypes()
        {
            try
            {
                var entities = Enum.GetValues(typeof(AccessType))
                     .Cast<AccessType>()
                     .Select(v => v.ToString())
                     .ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}