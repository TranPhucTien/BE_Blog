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

    [HttpGet("AllPublished")]
    public async Task<IActionResult> GetAllPublished([FromQuery] PostQueryObject query)
    {
        var posts = await _unitOfWork.PostRepository.GetAllFilterAsync(query);

        var postDtos = posts.Where(o => o.PublishedAt < DateTime.Now).Select(p => p.ToDto());

        return Ok(postDtos);
    }

    [HttpGet("AllByUser")]
    [Authorize]
    public async Task<IActionResult> GetAllPostsUser([FromQuery] PostUserQueryObject query)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        var posts = await _unitOfWork.PostRepository.GetAllPostsUserFilterAsync(user!.Id, query);

        var postDtos = posts.Select(p => p.ToDto());

        return Ok(postDtos);
    }

    [HttpGet("{postId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int postId)
    {
        var post = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
        {
            return NotFound();
        }

        var postDto = post.ToDto();

        return Ok(postDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);
        var postEntity = createPostDto.ToPostFromCreate(user!.Id);
        
        foreach (var tagId in createPostDto.TagIds)
        {
            var tagEntity = _unitOfWork.TagRepository.GetFirstOrDefaultAsync(o => o.Id == tagId).Result;
            
            if (tagEntity == null)
            {
                return BadRequest("Tag not found");
            }
            
            postEntity.PostTags.Add(new PostTag { Post = postEntity, Tag = tagEntity });
        }
        

        await _unitOfWork.PostRepository.AddAsync(postEntity);

        return CreatedAtAction(nameof(GetById), new { postId = postEntity.Id }, postEntity.ToDto());
    }

    [HttpPut("{postId:int}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] UpdatePostDto updatePostDto)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        if (!await IsUserOwnerOfPost(user!.Id, postId))
        {
            return Forbid("You are not the owner of this post.");
        }

        var postEntity = updatePostDto.ToPostFromUpdate(user.Id);
        var postTags = new List<PostTag>();
        
        foreach (var tagId in updatePostDto.TagIds)
        {
            var tagEntity = _unitOfWork.TagRepository.GetFirstOrDefaultAsync(o => o.Id == tagId).Result;
            
            if (tagEntity == null)
            {
                return BadRequest("Tag not found");
            }
            
            postTags.Add(new PostTag { Post = postEntity, Tag = tagEntity });
        }
        
        postEntity.PostTags = postTags;

        var post = await _unitOfWork.PostRepository.UpdateAsync(postId, postEntity);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post.ToDto());
    }

    [HttpDelete("{postId:int}")]
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