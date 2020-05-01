using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.Attributes;
using Pointwise.API.Admin.DTO;
using Pointwise.API.Admin.Roles;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using Pointwise.Domain.ServiceInterfaces;


namespace Pointwise.API.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService sourceService;
        private readonly IMapper mapper;
        public SourcesController(ISourceService sourceService, IMapper mapper)
        {
            this.sourceService = sourceService ?? throw new ArgumentNullException(nameof(sourceService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Attributes
        // GET: api/Sources
        /// <summary>
        /// Gets List of Sources
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SourceDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet]
        //[Authorize(Roles = CustomRoles.AdminOrAuthor)]
        [CustomAuthorize(EntityType.Source, AccessType.Select)]
        public IActionResult Get()
        {
            try
            {
                var entitiesDto = sourceService.GetSources()
                    .Select(x => mapper.Map<SourceDto>(x))
                    .ToList();

                if (entitiesDto.Any()) return Ok(entitiesDto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Gets All sources. Including soft-deleted ones.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("All")]
        [CustomAuthorize(EntityType.Source, AccessType.Select)]
        public IActionResult GetAll()
        {
            try
            {
                var entitiesDto = sourceService.GetSourcesAll()
                    .Select(x => mapper.Map<SourceDto>(x))
                    .ToList();

                if (entitiesDto.Any()) return Ok(entitiesDto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Searches source by text
        /// </summary>
        /// <param name="searchString">searchString is case-insensitive</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SourceDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("Search")]
        [CustomAuthorize(EntityType.Source, AccessType.Select)]
        public IActionResult GetBySearchString(string searchString)
        {
            try
            {
                var entitiesDto = sourceService.GetBySearchString(searchString)
                    .Select(x => mapper.Map<SourceDto>(x))
                    .ToList();

                if (entitiesDto.Any()) return Ok(entitiesDto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // GET: api/Sources/5
        /// <summary>
        /// Gets source by Id
        /// </summary>
        /// <param name="id">Id of Source</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SourceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("{id:int}", Name = "GetSourceById")]
        [CustomAuthorize(EntityType.Source, AccessType.Select)]
        public IActionResult GetById(int id)
        {
            try
            {
                var entityDto = mapper.Map<SourceDto>(sourceService.GetById(id));

                if (entityDto != null) return Ok(entityDto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // POST: api/Sources
        /// <summary>
        /// Creates a source
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost]
        [CustomAuthorize(EntityType.Source, AccessType.Add)]
        public IActionResult Create([FromBody]SourceDto source)
        {
            try
            {
                if (!ModelState.IsValid || source == null) return BadRequest(ModelState);
                var sourceExists = sourceService.Exist(source.Name);
                if (sourceExists)
                {
                    ModelState.AddModelError("", "Source Exists.");
                    return StatusCode(404, ModelState);
                }

                var addedEntity = sourceService.Add(mapper.Map<Source>(source));
                if(addedEntity == null)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving the source {source.Name}");
                    return StatusCode(500, ModelState);
                }
                var addedEntityDto = mapper.Map<SourceDto>(addedEntity);
                return CreatedAtRoute("GetSourceById", new { id = addedEntityDto.Id }, addedEntityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // PUT: api/Sources/5
        /// <summary>
        /// Updates a source
        /// </summary>
        /// <param name="id"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SourceDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPut("{id:int}")]
        [CustomAuthorize(EntityType.Source, AccessType.Update)]
        public IActionResult Update(int id, [FromBody]SourceDto source)
        {
            try
            {
                if (!ModelState.IsValid || source == null) return BadRequest(ModelState);

                source.Id = id;
                var updatedEntity = sourceService.Update(mapper.Map<Source>(source));

                if (updatedEntity != null) return Ok(mapper.Map<SourceDto>(updatedEntity));
                else
                {
                    ModelState.AddModelError("", $"Something went wrong while updating the source {source.Name}");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // DELETE: api/Sources/5
        /// <summary>
        /// Deletes a source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpDelete("{id:int}")]
        [CustomAuthorize(EntityType.Source, AccessType.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = sourceService.Delete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Soft-deletes a source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpDelete("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Source, AccessType.SoftDelete)]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                var status = sourceService.SoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Undoes soft-deletion of a source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Source, AccessType.UndoSoftDelete)]
        public IActionResult UndoSoftDelete(int id)
        {
            try
            {
                var status = sourceService.UndoSoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Delete Range
        ///// <summary>
        ///// Bulk deletes sources
        ///// </summary>
        ///// <param name="sources"></param>
        ///// <returns></returns>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        //[HttpDelete]
        //[Authorize(Roles = CustomRoles.AdminOrAuthor)]
        //public IActionResult Delete([FromBody]IEnumerable<Source> sources)
        //{
        //    try
        //    {
        //        var status = sourceService.DeleteRange(sources);
        //        if (status) return Ok();
        //        else return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion
        #region Soft-delete Range
        ///// <summary>
        ///// Bulk soft-deletes sources
        ///// </summary>
        ///// <param name="sources"></param>
        ///// <returns></returns>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        //[HttpDelete("SoftDeleteRange")]
        //[Authorize(Roles = CustomRoles.AdminOrAuthor)]
        //public IActionResult SoftDelete([FromBody]IEnumerable<Source> sources)
        //{
        //    try
        //    {
        //        var status = sourceService.SoftDeleteRange(sources);
        //        if (status) return Ok();
        //        else return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion
    }
}