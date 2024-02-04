using Simp.Modules.Blogs.Domain.Blogs.DomainEvents;
using Simp.Shared.Abstractions.Primitives;

namespace Simp.Modules.Blogs.Domain.Blogs;
public class Blog : AggregateRoot
{
    protected Blog(Guid id, string title, string description, string content, string contentHtml, bool published) : base(id)
    {
        Title = title;
        Description = description;
        Content = content;
        ContentHtml = contentHtml;
        Published = published;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Content { get; private set; }
    public string ContentHtml { get; private set; }
    public bool Published { get; private set; }

    public static Blog Create(string title, string description, string content, string contentHtml, bool published)
    {
        var blog = new Blog(Guid.NewGuid(), title, description, content, contentHtml, published);

        blog.RaiseDomainEvent(new BlogCreated(blog.Id));

        return blog;
    }

    public void Update(string title, string description, string content, string contentHtml)
    {
        Title = title;
        Description = description;
        Content = content;
        ContentHtml = contentHtml;
    }

    public void UpdatePublished(bool published)
    {
        Published = published;
    }
}
