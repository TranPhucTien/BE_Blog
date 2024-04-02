using Blog.Core.Extensions;
using Blog.Core.Helpers;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Comment;
using Blog.Models.Entities;
using Blog.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CommentController(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentsByPost([FromQuery] CommentQueryObject query)
    {
        var comments = await _unitOfWork.CommentRepository.GetAllFilterAsync(query);

        return Ok(comments.ToPaginationFromListPost(query.PageNumber, query.PageSize));
    }

    public object FromQuery { get; set; }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddComment([FromBody] CreateCommentDto comment)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        var commentModel = comment.ToCommentFromCreate(user.Id);
        await _unitOfWork.CommentRepository.AddAsync(commentModel);

        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto newComment)
    {
        var commentModel = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(c => c.Id == newComment.Id);

        if (commentModel == null)
        {
            return NotFound();
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        if (commentModel.UserId != user.Id)
        {
            return Forbid("You are not the owner of this comment.");
        }

        await _unitOfWork.CommentRepository.UpdateAsync(commentModel, newComment);

        return Ok();
    }

    [HttpDelete("{commentId:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        var comment = await _unitOfWork.CommentRepository.GetFirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return NotFound();
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        if (comment.UserId != user.Id)
        {
            return Forbid("You are not the owner of this comment.");
        }

        await _unitOfWork.CommentRepository.DeleteAsync(comment);

        return Ok();
    }
}