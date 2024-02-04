namespace Simp.Modules.Blogs.Contracts.Blogs;
public record BlogRequest(string Title, string Description, string Content, bool Published, bool IsTemplate);