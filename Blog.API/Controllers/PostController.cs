using Blog.Core.Extensions;
using Blog.Core.Helpers;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;
using Blog.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    public PostController(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PostQueryObject query)
    {
        var posts = await _unitOfWork.PostRepository.GetAllFilterAsync(query);
        
        var postDtos = posts.Select(p => p.ToDto());
        
        return Ok(postDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById([FromRoute] int id)
    {
        var post = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        var postDto = post.ToDto();
        
        return Ok(postDto);
    }
    
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = User.GetUsername();
        
        if (username == null)
        {
            return Unauthorized("Phải đăng nhập để tạo bài viết");
        }
        
        var user = await _userManager.FindByNameAsync(username);
        
        if (user == null)
        {
            return Unauthorized("Tài khoản không tồn tại");
        }
        
        var postEntity = createPostDto.ToPostFromCreate(user.Id);
        
        await _unitOfWork.PostRepository.AddAsync(postEntity);
        
        return CreatedAtAction(nameof(GetPostById), new { id = postEntity.Id }, postEntity.ToDto());
    }
    
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var post = await _unitOfWork.PostRepository.UpdateAsync(id, updatePostDto);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(post.ToDto());
    }
    
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeletePost([FromRoute] int id)
    {
        var post = await _unitOfWork.PostRepository.DeleteAsync(id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(post.ToDto());
    }
}