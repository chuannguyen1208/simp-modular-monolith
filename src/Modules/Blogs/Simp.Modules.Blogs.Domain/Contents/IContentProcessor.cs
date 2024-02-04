namespace Simp.Modules.Blogs.Domain.Contents;

public interface IContentProcessor
{
    Task<string> ProcessContent(string content);
}