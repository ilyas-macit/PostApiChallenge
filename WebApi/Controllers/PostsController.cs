using Business.Abstracts;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = _postService.GetAll();
            return Ok(result);
        }


        [HttpGet("GetById")]
        public IActionResult GetById(string postId)
        {
            try
            {
                var result = _postService.GetById(postId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost(CreatePostDto postDto)
        {
            try
            {
                if (postDto != null)
                {
                    Post post = new Post
                    {
                        Title = postDto.Title,
                        Description = postDto.Description,
                        ShortDescription = postDto.ShortDescription,
                        Tags = postDto.Tags,
                        CreatedAt = DateTime.Now
                    };
                    _postService.Create(post);
                }

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpPost("Update")]
        public IActionResult Update(UpdatePostDto postDto)
        {
            try
            {
                if (postDto != null)
                {
                    Post post = new Post
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        ShortDescription = postDto.ShortDescription,
                        Description = postDto.Description,
                        Tags = postDto.Tags,
                        UpdatedAt = DateTime.Now
                    };

                    _postService.Update(post);
                    return Ok();
                }
                throw new Exception("PostDto is null!");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string postId)
        {
            try
            {
                _postService.Delete(postId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
