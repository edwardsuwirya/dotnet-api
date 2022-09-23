using AutoMapper;
using MySimpleNetApi.Models;
using MySimpleNetApi.Resource;

namespace MySimpleNetApi.Mapping;

public class RequestToModelProfile : Profile
{
    public RequestToModelProfile()
    {
        CreateMap<RegisterCategoryRequest, Category>();
        CreateMap<RegisterProductRequest, Product>();
    }
}