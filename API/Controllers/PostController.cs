using Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Business.ServicesInterfaces;
using PostEntity = DataAccess.Data.Post;
using System.Collections.Generic;

namespace API.Controllers.Post
{
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        protected IPostService<PostEntity> _postService;
        public PostController(IPostService<PostEntity> postService)
        {
            _postService = postService;
        }

        [HttpGet()]
        public IQueryable<PostEntity> GetAll()
        {
            return _postService.GetAll();
        }

        [HttpPost()]
        public IActionResult Create([FromBodyAttribute] PostEntity entity)
        {
            try
            {
                var response = _postService.Create(entity.CustomerId, entity);
                if (response == null)
                {
                    return BadRequest("No existe customer asociado.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el cliente: " + ex.Message);
            }
        }


        [HttpPost("create-multiple-posts")]
        public IActionResult CreateMultiplePosts([FromBody] List<PostEntity> request)
        {
            try
            {
                foreach (var postEntity in request)
                {
                    var response = _postService.Create(postEntity.CustomerId, postEntity);

                    if (response == null)
                    {
                        return BadRequest("No existe customer asociado");
                    }
                }

                return Ok("Posts creados exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear los posts: " + ex.Message);
            }
        }
    }
}
