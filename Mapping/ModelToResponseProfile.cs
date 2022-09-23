using AutoMapper;
using MySimpleNetApi.Models;
using MySimpleNetApi.Resource;

namespace MySimpleNetApi.Mapping;

public class ModelToResponseProfile : Profile
{
    public ModelToResponseProfile()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<Product, ProductResponse>();
    }
}