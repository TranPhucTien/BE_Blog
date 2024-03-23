using Blog.Core.Helpers;
using Blog.DataAccess.Repositories;
using Blog.Models.DTOs.Post;
using Blog.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APIs.Posts;

[Route("api/[controller]")]
[ApiController]
public class PostController(UnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PostQueryObject query)
    {
        var posts = await unitOfWork.PostRepository.GetAllFilterAsync(query);
        
        var postDtos = posts.Select(p => p.ToDto());
        
        return Ok(postDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById([FromRoute] int id)
    {
        var post = await unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        var postDto = post.ToDto();
        
        return Ok(postDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var postEntity = createPostDto.ToEntity();
        
        await unitOfWork.PostRepository.AddAsync(postEntity);
        
        
        return CreatedAtAction(nameof(GetPostById), new { id = postEntity.Id }, postEntity.ToDto());
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var post = await unitOfWork.PostRepository.UpdateAsync(id, updatePostDto);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(post.ToDto());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id)
    {
        var post = await unitOfWork.PostRepository.DeleteAsync(id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(post.ToDto());
    }
}