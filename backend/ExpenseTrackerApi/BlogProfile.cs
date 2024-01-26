using AutoMapper;
using Contracts.Dto.Blogs;
using Domain.Models;

namespace ExpenseTrackerApi;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogDto>();
        CreateMap<Blog, SingleBlogDto>();
    }
}