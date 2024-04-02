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
}