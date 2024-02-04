using Markdig;
using Simp.Modules.Blogs.Domain.Contents;

namespace Simp.Modules.Blogs.Infrastructure.Contents;
internal class ContentProcessor : IContentProcessor
{
    private readonly MarkdownPipeline _markdownPipeline;

    public ContentProcessor()
    {
        _markdownPipeline = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseAdvancedExtensions()
            .Build();
    }

    public async Task<string> ProcessContent(string content)
    {
        var html = Markdown.ToHtml(content, _markdownPipeline);
        return await Task.FromResult(html);
    }
}
