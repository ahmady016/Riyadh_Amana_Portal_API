using AutoMapper;
using DB.Entities;
using Dtos;

namespace Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserInput>().ReverseMap();
        CreateMap<User, UpdateUserInput>().ReverseMap();

        CreateMap<Advertisement, AdvertisementDto>().ReverseMap();
        CreateMap<Advertisement, CreateAdvertisementInput>().ReverseMap();
        CreateMap<Advertisement, UpdateAdvertisementInput>().ReverseMap();

        CreateMap<Article, ArticleDto>().ReverseMap();
        CreateMap<Article, CreateArticleInput>().ReverseMap();
        CreateMap<Article, UpdateArticleInput>().ReverseMap();

        CreateMap<News, NewsDto>().ReverseMap();
        CreateMap<News, CreateNewsInput>().ReverseMap();
        CreateMap<News, UpdateNewsInput>().ReverseMap();

    }
}
