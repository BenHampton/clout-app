using AutoMapper;

namespace clout_api.Extensions.Mapper;

public partial class AutoMapperProfileExtensions : Profile
{
    public AutoMapperProfileExtensions()
    {
        CreateDtoMaps();
    }
}
