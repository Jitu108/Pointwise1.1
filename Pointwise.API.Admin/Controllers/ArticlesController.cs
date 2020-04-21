using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pointwise.API.Admin.DTO;
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

        public ArticlesController(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
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

        [HttpGet("Author")]
        public IActionResult GetByAuthor(string author)
        {
            try
            {
                var entities = articleService.GetArticlesByAuthor(author)
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

        [HttpGet("Title")]
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

        [HttpGet("Content")]
        public IActionResult GetByContent(string contentString)
        {
            try
            {
                var entities = articleService.GetArticleByContent(contentString)
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
        public IActionResult Create([FromBody]ArticleDto article)
        {
            try
            {
                if (!ModelState.IsValid || article == null) return BadRequest(ModelState);
                var art = mapper.Map<Article>(article);
                var addedEntity = articleService.Add(art);
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