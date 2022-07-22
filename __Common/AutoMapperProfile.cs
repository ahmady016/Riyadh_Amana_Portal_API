using AutoMapper;

using Dtos;
using Auth.Dtos;
using DB.Entities;

namespace Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, RegisterInput>().ReverseMap();
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

        CreateMap<ContactUs, ContactUsDto>().ReverseMap();
        CreateMap<ContactUs, CreateContactUsInput>().ReverseMap();
        CreateMap<ContactUs, UpdateContactUsInput>().ReverseMap();

        CreateMap<Award, AwardDto>().ReverseMap();
        CreateMap<Award, CreateAwardInput>().ReverseMap();
        CreateMap<Award, UpdateAwardInput>().ReverseMap();

        CreateMap<Partner, PartnerDto>().ReverseMap();
        CreateMap<Partner, CreatePartnerInput>().ReverseMap();
        CreateMap<Partner, UpdatePartnerInput>().ReverseMap();

        CreateMap<Document, DocumentDto>().ReverseMap();
        CreateMap<Document, CreateDocumentInput>().ReverseMap();
        CreateMap<Document, UpdateDocumentInput>().ReverseMap();

    }
}
