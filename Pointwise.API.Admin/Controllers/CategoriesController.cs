using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private int loggedInUserId;
        public CategoriesController(ICategoryService categoryService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            this.loggedInUserId = Int32.Parse(userid);
        }

        #region Attributes
        // GET: api/Categories
        /// <summary>
        /// Gets List of Categories
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet]
        [CustomAuthorize(EntityType.Category, AccessType.Select)]
        public IActionResult Get()
        {
            try
            {
                var entitiesdto = categoryService.GetCategories()
                    .Select(x => mapper.Map<CategoryDto>(x))
                    .ToList();

                if (entitiesdto.Any()) return Ok(entitiesdto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Gets All categorys. Including soft-deleted ones.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("All")]
        [CustomAuthorize(EntityType.Category, AccessType.Select)]
        public IActionResult GetAll()
        {
            try
            {
                var entities = categoryService.GetCategoriesAll()
                    .Select(x => mapper.Map<CategoryDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        /// <summary>
        /// Searches category by text
        /// </summary>
        /// <param name="searchString">searchString is case-insensitive</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("Search")]
        [CustomAuthorize(EntityType.Category, AccessType.Select)]
        public IActionResult GetBySearchString(string searchString)
        {
            try
            {
                var entities = categoryService.GetBySearchString(searchString)
                    .Select(x => mapper.Map<CategoryDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // GET: api/Categories/5
        /// <summary>
        /// Gets category by Id
        /// </summary>
        /// <param name="id">Id of Category</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [CustomAuthorize(EntityType.Category, AccessType.Select)]
        public IActionResult GetById(int id)
        {
            try
            {
                var entitydto = mapper.Map<CategoryDto>(categoryService.GetById(id));

                if (entitydto != null) return Ok(entitydto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // POST: api/Categories
        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost]
        [CustomAuthorize(EntityType.Category, AccessType.Add)]
        public IActionResult Create([FromBody]CategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid || category == null) return BadRequest(ModelState);
                var categoryExists = categoryService.Exist(category.Name);
                if (categoryExists)
                {
                    ModelState.AddModelError("", "Category Exists.");
                    return StatusCode(404, ModelState);
                }
                var domainEntity = mapper.Map<Category>(category);
                domainEntity.CreatedBy = loggedInUserId;

                var addedEntity = categoryService.Add(domainEntity);
                if (addedEntity == null)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving the category {category.Name}");
                    return StatusCode(500, ModelState);
                }
                var addedEntityDto = mapper.Map<CategoryDto>(addedEntity);
                return CreatedAtRoute("GetCategoryById", new { id = addedEntityDto.Id }, addedEntityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // PUT: api/Categories/5
        /// <summary>
        /// Updates a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPut("{id:int}")]
        [CustomAuthorize(EntityType.Category, AccessType.Update)]
        public IActionResult Update(int id, [FromBody]CategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid || category == null) return BadRequest(ModelState);

                category.Id = id;
                var updatedEntity = categoryService.Update(mapper.Map<Category>(category));

                if (updatedEntity != null) return Ok(mapper.Map<CategoryDto>(updatedEntity));
                else
                {
                    ModelState.AddModelError("", $"Something went wrong while updating the category {category.Name}");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Attributes
        // DELETE: api/Categories/5
        /// <summary>
        /// Deletes a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpDelete("{id:int}")]
        [CustomAuthorize(EntityType.Category, AccessType.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = categoryService.Delete(id);
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
        /// Soft-deletes a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpDelete("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Category, AccessType.SoftDelete)]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                var status = categoryService.SoftDelete(id);
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
        /// Undoes soft-deletion of a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Category, AccessType.UndoSoftDelete)]
        public IActionResult UndoSoftDelete(int id)
        {
            try
            {
                var status = categoryService.UndoSoftDelete(id);
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