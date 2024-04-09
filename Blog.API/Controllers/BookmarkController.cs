using Blog.Core.Extensions;
using Blog.Core.Helpers;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Bookmark;
using Blog.Models.Entities;
using Blog.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookmarkController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public BookmarkController(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
    }

    [HttpGet("AllByUser")]
    [Authorize]
    public async Task<IActionResult> GetAllBookmarksUser([FromQuery] BookmarkUserQueryObject query)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        var bookmarks = await _unitOfWork.BookmarkRepository.GetAllFilterAsync(user!.Id, query);

        return Ok(bookmarks.ToPaginationFromListPost(query.PageNumber, query.PageSize));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddBookmark([FromBody] CreateBookmarkDto bookmark)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username!);

        var bookmarkAdded = await _unitOfWork.BookmarkRepository.AddAsync(bookmark.ToEntity(user!.Id));

        // increase bookmark count in post
        var post = await _unitOfWork.PostRepository.GetFirstOrDefaultAsync(p => p.Id == bookmark.PostId);

        post.Bookmarks++;

        _unitOfWork.PostRepository.UpdateAsync(post.Id, post);

        return Ok(bookmarkAdded);
    }
}