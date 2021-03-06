﻿using AutoMapper;
using Pointwise.Common.DTO;
using Pointwise.Common.Models;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pointwise.Common.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Source, SourceDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            //CreateMap<User, UserDto>().ReverseMap();
        }
    }

    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            RecognizeDestinationPrefixes("Article");
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ArticleCategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.ArticleCategory, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ArticleSourceId, opt => opt.MapFrom(src => src.Source.Id))
                .ForMember(dest => dest.ArticleSource, opt => opt.MapFrom(src => src.Source.Name))
                .ForMember(dest => dest.ArticleTags, opt => opt.MapFrom(src => src.Tags.Select(x => x.Name).ToList()))
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Image.Id))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.Image.Name))
                .ForMember(dest => dest.ImageCaption, opt => opt.MapFrom(src => src.Image.Caption))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image.Path))
                .ForMember(dest => dest.ImageContentType, opt => opt.MapFrom(src => src.Image.ContentType))
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => System.Text.Encoding.UTF8.GetString(src.Image.Data)))
                .ForMember(dest => dest.ImageExtension, opt => opt.MapFrom(src => Enum.GetName(src.Image.Extension.GetType(), src.Image.Extension)))
                .ForMember(dest => dest.ImageSavedTo, opt => opt.MapFrom(src => Enum.GetName(src.Image.SavedTo.GetType(), src.Image.SavedTo)));

            RecognizePrefixes("Article");
            CreateMap<ArticleDto, Article>()
                .ForMember(dest => dest.Source, act => act.MapFrom(src => new Source { Id = src.ArticleSourceId, Name = src.ArticleSource }))
                .ForMember(dest => dest.Category, act => act.MapFrom(src => new Category { Id = src.ArticleCategoryId, Name = src.ArticleCategory }))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ArticleTags.Select(x => new Tag { Name = x })))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new Image
                {
                    Id = src.ImageId,
                    Name = src.ImageName,
                    Caption = src.ImageCaption,
                    Path = src.ImagePath,
                    ContentType = src.ImageContentType,
                    Data = src.ImageData != null ? Encoding.ASCII.GetBytes(src.ImageData) : Array.Empty<byte>(),
                    Extension = src.ImageExtension != null ? (Extension)Enum.Parse(typeof(Extension), src.ImageExtension) : Extension.None,
                    SavedTo = src.ImageSavedTo != null ? (ImageSaveTo)Enum.Parse(typeof(ImageSaveTo), src.ImageSavedTo) : ImageSaveTo.None
                }));
        }
    }

    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<AuthUser, AuthUserDto>()
                .ForMember(dest => dest.UserNameType, opt => opt.MapFrom(src => Enum.GetName(typeof(UserNameType), src.UserNameType)))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.Name))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(y => new Role { AccessType = y.AccessTypeName, EntityType = y.EntityTypeName }).ToList()));

            CreateMap<AuthUserDto, AuthUser>()
                .ForMember(dest => dest.UserNameType, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.UserNameType) ? UserNameType.Custom : (UserNameType)Enum.Parse(typeof(UserNameType), src.UserNameType, true)))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => new UserType { Name = src.UserType }))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(y => new UserRole
                {
                    EntityType = (EntityType)Enum.Parse(typeof(EntityType), y.EntityType, true),
                    AccessType = (AccessType)Enum.Parse(typeof(AccessType), y.AccessType, true)
                })));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserNameType, opt => opt.MapFrom(src => Enum.GetName(typeof(UserNameType), src.UserNameType)))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.Name))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(y => new Role { AccessType = y.AccessTypeName, EntityType = y.EntityTypeName }).ToList()));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserNameType, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.UserNameType) ? UserNameType.Custom : (UserNameType)Enum.Parse(typeof(UserNameType), src.UserNameType, true)))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => new UserType { Name = src.UserType }))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(y => new UserRole
                {
                    EntityType = (EntityType)Enum.Parse(typeof(EntityType), y.EntityType, true),
                    AccessType = (AccessType)Enum.Parse(typeof(AccessType), y.AccessType, true)
                })));
        }
    }
}
