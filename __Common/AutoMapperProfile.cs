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

        CreateMap<AppFeature, AppFeatureDto>().ReverseMap();
        CreateMap<AppFeature, CreateAppFeatureInput>().ReverseMap();
        CreateMap<AppFeature, UpdateAppFeatureInput>().ReverseMap();

        CreateMap<Album, AlbumDto>().ReverseMap();
        CreateMap<Album, CreateAlbumInput>().ReverseMap();
        CreateMap<Album, CreateAlbumWithPhotosInput>().ReverseMap();
        CreateMap<Album, UpdateAlbumInput>().ReverseMap();

        CreateMap<Photo, PhotoDto>().ReverseMap();
        CreateMap<Photo, CreatePhotoInput>().ReverseMap();
        CreateMap<Photo, UpdatePhotoInput>().ReverseMap(); 

        CreateMap<AppPage, AppPageDto>().ReverseMap();
        CreateMap<AppPage, CreateAppPageInput>().ReverseMap();
        CreateMap<AppPage, CreateAppPageWithKeysInput>().ReverseMap();
        CreateMap<AppPage, UpdateAppPageInput>().ReverseMap();

        CreateMap<PageKey, PageKeyDto>().ReverseMap();
        CreateMap<PageKey, CreatePageKeyInput>().ReverseMap();
        CreateMap<PageKey, UpdatePageKeyInput>().ReverseMap();

        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentInput>().ReverseMap();
        CreateMap<Comment, UpdateCommentInput>().ReverseMap();

        CreateMap<Reply, ReplyDto>().ReverseMap();
        CreateMap<Reply, CreateReplyInput>().ReverseMap();
        CreateMap<Reply, UpdateReplyInput>().ReverseMap();

        CreateMap<Nav, NavDto>().ReverseMap();
        CreateMap<Nav, CreateNavInput>().ReverseMap();
        CreateMap<Nav, CreateNavWithLinksInput>().ReverseMap();
        CreateMap<Nav, UpdateNavInput>().ReverseMap();

        CreateMap<NavLink, NavLinkDto>().ReverseMap();
        CreateMap<NavLink, CreateNavLinkInput>().ReverseMap();
        CreateMap<NavLink, CreateNavLinkWithNavIdInput>().ReverseMap();
        CreateMap<NavLink, UpdateNavLinkInput>().ReverseMap();

        CreateMap<Video, VideoDto>().ReverseMap();
        CreateMap<Video, CreateVideoInput>().ReverseMap();
        CreateMap<Video, UpdateVideoInput>().ReverseMap();

    }
}
