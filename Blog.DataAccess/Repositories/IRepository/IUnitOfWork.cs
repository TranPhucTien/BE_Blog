namespace Blog.DataAccess.Repositories.IRepository;

public interface IUnitOfWork
{

    IPostRepository PostRepository { get; }

    ITagRepository TagRepository { get; }

    ICommentRepository CommentRepository { get; }

    IBookmarkRepository BookmarkRepository { get; }

    IPostTagRepository PostTagRepository { get; }
}