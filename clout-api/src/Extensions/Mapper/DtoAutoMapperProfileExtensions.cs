// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.Comment;
using clout_api.Data.Dtos.Friend;
using clout_api.Data.Dtos.FriendRequest;
using clout_api.Data.Dtos.Post;
using clout_api.Data.Dtos.User;
using clout_api.Data.Dtos.UserFriend;
using clout_api.Data.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace clout_api.Extensions.Mapper;

public partial class AutoMapperProfileExtensions : Profile
{
    public void CreateDtoMaps()
    {
        //reference -> CreateMap<TSource, TDestination>

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Friends, opt => opt.MapFrom(src => src.UserFriends));
        CreateMap<User, SearchUserDto>();
        CreateMap<RequestUserDto, User>();
        CreateMap<User, FriendDto>();

        CreateMap<UserFriend, UserFriendDto>();
        CreateMap<UserFriendRequestDto, UserFriend>();
        CreateMap<RequestFriendRequestDto, UserFriend>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserIdOne))
            .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.UserIdTwo))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Friend, opt => opt.Ignore());

        CreateMap<UserFriend, MiniFriendDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FriendId))
            .ForMember(dest => dest.FirstName, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.Ignore());

        CreateMap<UserFriend, FriendDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FriendId))
            .ForMember(dest => dest.FirstName, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.Ignore());


        CreateMap<FriendRequest, FriendRequestDto>();
        CreateMap<FriendRequest, ResponseFriendRequestDto>();
        CreateMap<RequestFriendRequestDto, FriendRequest>();
        CreateMap<UserFriend, ResponseFriendRequestDto>()
            .ForMember(dest => dest.UserIdOne, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserIdTwo, opt => opt.MapFrom(src => src.FriendId));
        CreateMap<UserFriend, FriendRequestDto>()
            .ForMember(dest => dest.UserIdOne, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserIdTwo, opt => opt.MapFrom(src => src.FriendId));


        CreateMap<Post, PostDto>();
        CreateMap<RequestPostDto, Post>();

        CreateMap<Comment, CommentDto>();
        CreateMap<RequestCommentDto, Comment>();
    }
}
