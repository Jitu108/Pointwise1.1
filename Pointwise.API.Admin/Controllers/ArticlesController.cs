using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.Attributes;
using Pointwise.Common.DTO;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.API.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private int loggedInUserId;

        public ArticlesController(IArticleService articleService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            this.loggedInUserId = Int32.Parse(userid);
        }

        [HttpGet]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult Get()
        {
            try
            {
                var entities = articleService.GetAllArticles();
                var entitiesdto = entities
                    .Select(x => mapper.Map<ArticleDto>(x))
                    .ToList();

                if (entitiesdto.Any()) return Ok(entitiesdto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Title")]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult GetByTitle(string titleString)
        {
            try
            {
                var entities = articleService.GetArticleByTitle(titleString)
                    .Select(x => mapper.Map<ArticleDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Desc")]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult GetByDesc(string descString)
        {
            try
            {
                var entities = articleService.GetArticleByDescription(descString)
                    .Select(x => mapper.Map<ArticleDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Source")]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult GetBySource(int sourceId)
        {
            try
            {
                var entities = articleService.GetArticleBySource(sourceId)
                    .Select(x => mapper.Map<ArticleDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Category")]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult GetByCategory(int categoryId)
        {
            try
            {
                var entities = articleService.GetArticleByCategory(categoryId)
                    .Select(x => mapper.Map<ArticleDto>(x))
                    .ToList();

                if (entities.Any()) return Ok(entities);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetArticleById")]
        [CustomAuthorize(EntityType.Article, AccessType.Select)]
        public IActionResult GetById(int id)
        {
            try
            {
                var entitydto = mapper.Map<ArticleDto>(articleService.GetById(id));

                if (entitydto != null) return Ok(entitydto);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [CustomAuthorize(EntityType.Article, AccessType.Add)]
        public IActionResult Create([FromBody]ArticleDto article)
        {
            try
            {
                if (!ModelState.IsValid || article == null) return BadRequest(ModelState);

                var domainEntity = mapper.Map<Article>(article);
                domainEntity.CreatedBy = loggedInUserId;

                var addedEntity = articleService.Add(domainEntity);
                if (addedEntity == null)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving the article {article.ArticleTitle}");
                    return StatusCode(500, ModelState);
                }
                var addedEntityDto = mapper.Map<ArticleDto>(addedEntity);
                return CreatedAtRoute("GetArticleById", new { id = addedEntityDto.ArticleId }, addedEntityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [CustomAuthorize(EntityType.Article, AccessType.Update)]
        public IActionResult Update(int id, [FromBody]ArticleDto article)
        {
            try
            {
                if (!ModelState.IsValid || article == null) return BadRequest(ModelState);

                article.ArticleId = id;
                var updatedEntity = articleService.Update(mapper.Map<Article>(article));

                if (updatedEntity != null) return Ok(mapper.Map<ArticleDto>(updatedEntity));
                else
                {
                    ModelState.AddModelError("", $"Something went wrong while updating the article {article.ArticleTitle}");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [CustomAuthorize(EntityType.Article, AccessType.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var status = articleService.Delete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Article, AccessType.SoftDelete)]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                var status = articleService.SoftDelete(id);
                if (status) return Ok();
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("[action]/{id:int}")]
        [CustomAuthorize(EntityType.Article, AccessType.UndoSoftDelete)]
        public IActionResult UndoSoftDelete(int id)
        {
            try
            {
                var status = articleService.UndoSoftDelete(id);
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