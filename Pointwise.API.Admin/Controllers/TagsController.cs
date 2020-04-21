using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.DTO;
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

        public TagsController(ITagService tagService, IMapper mapper)
        {
            this.tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
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

                var addedEntity = tagService.Add(mapper.Map<Tag>(tag));
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