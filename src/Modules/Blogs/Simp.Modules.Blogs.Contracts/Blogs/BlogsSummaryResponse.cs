namespace Simp.Modules.Blogs.UseCases.Blogs.Queries;

public record BlogsSummaryResponse(int BlogsCount, int TemplatesCount, int SubcriptionsCount)
{
}