using AutoMapper;
using Simp.Modules.Blogs.Contracts.Blogs;
using Simp.Modules.Blogs.Domain.Blogs;

namespace Simp.Modules.Blogs.UseCases.Blogs;
public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogResponse>();
    }
}
