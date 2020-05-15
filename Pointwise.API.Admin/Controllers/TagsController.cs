using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.Attributes;
using Pointwise.API.Admin.DTO;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.API.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IMapper mapper;
        private int loggedInUserId;
        public TagsController(ITagService tagService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            this.loggedInUserId = Int32.Parse(userid);
        }

        [HttpGet]
        [CustomAuthorize(EntityType.Tag, AccessType.Select)]
        public IActionResult Get()
        {
            try
            {
                var entitiesdto = tagService.GetTags()
                    .Select(x => mapper.Map<TagDto>(x))
                    .ToList();

                if (entitiesdto.Any()) return Ok(entitiesdto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        [CustomAuthorize(EntityType.Tag, AccessType.Select)]
        public IActionResult GetAll()
        {
            try
            {
                var entities = tagService.GetTagsAll()
                    .Select(x => mapper.Map<TagDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        [CustomAuthorize(EntityType.Tag, AccessType.Select)]
        public IActionResult GetBySearchString(string searchString)
        {
            try
            {
                var entities = tagService.GetBySearchString(searchString)
                    .Select(x => mapper.Map<TagDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetTagById")]
        [CustomAuthorize(EntityType.Tag, AccessType.Select)]
        public IActionResult GetById(int id)
        {
            try
            {
                var entitydto = mapper.Map<TagDto>(tagService.GetById(id));

                if (entitydto != null) return Ok(entitydto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CustomAuthorize(EntityType.Tag, AccessType.Add)]
        public IActionResult Create([FromBody]TagDto tag)
        {
            try
            {
                if (!ModelState.IsValid || tag == null) return BadRequest(ModelState);
                var tagExists = tagService.Exist(tag.Name);
                if (tagExists)
                {
                    ModelState.AddModelError("", "Tag Exists.");
                    return StatusCode(404, ModelState);
                }
                var domainEntity = mapper.Map<Tag>(tag);
                domainEntity.CreatedBy = loggedInUserId;

                var addedEntity = tagService.Add(domainEntity);
                if (addedEntity == null)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving the tag {tag.Name}");
                    return StatusCode(500, ModelState);
                }
                var addedEntityDto = mapper.Map<TagDto>(addedEntity);
                return CreatedAtRoute("GetTagById", new { id = addedEntityDto.Id }, addedEntityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [CustomAuthorize(EntityType.Tag, AccessType.Update)]
        public IActionResult Update(int id, [FromBody]TagDto tag)
        {
            try
            {
                if (!ModelState.IsValid || tag == null) return BadRequest(ModelState);

                tag.Id = id;
                var updatedEntity = tagService.Update(mapper.Map<Tag>(tag));

                if (updatedEntity != null) return Ok(mapper.Map<TagDto>(updatedEntity));
                else
                {
                    ModelState.AddModelError("", $"Something went wrong while updating the tag {tag.Name}");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [CustomAuthorize(EntityType.Tag, AccessType.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = tagService.Delete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Tag, AccessType.SoftDelete)]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                var status = tagService.SoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Tag, AccessType.UndoSoftDelete)]
        public IActionResult UndoSoftDelete(int id)
        {
            try
            {
                var status = tagService.UndoSoftDelete(id);
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