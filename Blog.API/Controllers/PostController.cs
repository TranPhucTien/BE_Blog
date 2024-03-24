using Blog.Core.Extensions;
using Blog.Core.Helpers;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;
using Blog.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("GetAllPublished")]
    public async Task<IActionResult> GetAllPublished([FromQuery] PostQueryObject query)
    {
        var posts = await _unitOfWork.PostRepository.GetAllFilterAsync(query);

        var postDtos = posts.Where(o => o.PublishedAt < DateTime.Now).Select(p => p.ToDto());

        return Ok(postDtos);
    }

    [HttpGet("GetPostsOfUser")]
    [Authorize]
    public async Task<IActionResult> GetAllPostsUser([FromQuery] PostUserQueryObject query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        var posts = await _unitOfWork.PostRepository.GetAllPostsUserFilterAsync(user!.Id, query);

        var postDtos = posts.Select(p => p.ToDto());

        return Ok(postDtos);
    }

    [HttpGet("GetById/{postId:int}")]
    public async Task<IActionResult> GetPostById([FromRoute] int postId)
    {
        var post = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
        {
            return NotFound();
        }

        var postDto = post.ToDto();

        return Ok(postDto);
    }

    [HttpPost("Create")]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);
        var postEntity = createPostDto.ToPostFromCreate(user!.Id);

        await _unitOfWork.PostRepository.AddAsync(postEntity);

        return CreatedAtAction(nameof(GetPostById), new { id = postEntity.Id }, postEntity.ToDto());
    }

    [HttpPut("Update/{postId:int}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] UpdatePostDto updatePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        if (!await IsUserOwnerOfPost(user!.Id, postId))
        {
            return Forbid("You are not the owner of this post.");
        }

        var post = await _unitOfWork.PostRepository.UpdateAsync(postId, updatePostDto.ToPostFromUpdate(user!.Id));

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post.ToDto());
    }

    [HttpDelete("Delete/{postId:int}")]
    [Authorize]
    public async Task<IActionResult> DeletePost([FromRoute] int postId)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        if (!await IsUserOwnerOfPost(user!.Id, postId))
        {
            return Forbid("You are not the owner of this post.");
        }

        var post = await _unitOfWork.PostRepository.DeleteAsync(postId);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post.ToDto());
    }

    private async Task<bool> IsUserOwnerOfPost(string userId, int postId)
    {
        var post = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
        {
            throw new Exception("Post not found");
        }

        return post.AuthorId == userId;
    }
}