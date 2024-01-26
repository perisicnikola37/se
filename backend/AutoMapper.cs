public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Blog, BlogDto>()
            .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src));

    }
}
