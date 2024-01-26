namespace Simp.Modules.Blogs.Contracts.Blogs;
public record BlogResponse(Guid Id, string Title, string Description, string Content, bool Published)
{
}
