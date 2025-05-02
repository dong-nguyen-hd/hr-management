using AutoMapper;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Account;
using Business.Resources.Authentication;
using Business.Resources.Information;
using Microsoft.Extensions.Options;

namespace Business.Mapping.Account;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Account, AccountResource>()
                .ForMember(x => x.Groups, opt => opt.MapFrom(src => src.Groups))
                .ForMember(x => x.Avatar, opt => opt.MapFrom<CustomResolver>());

            CreateMap<Domain.Models.Account, AccessTokenResource>()
                .ForMember(x => x.Avatar, opt => opt.MapFrom<CustomResolver>());
        }
}

/// <summary>
/// Custom Value Resolvers
/// </summary>
class CustomResolver : IValueResolver<Domain.Models.Account, AccountResource, AvatarResource>
{
    #region Property
    private readonly IUriService _uriService;
    private readonly HostResource _hostResource;
    #endregion

    #region Constructor
    public CustomResolver(IUriService uriService, IOptionsSnapshot<HostResource> hostResource)
    {
            this._uriService = uriService;
            this._hostResource = hostResource.Value;
        }
    #endregion

    #region Method
    public AvatarResource Resolve(Domain.Models.Account source, AccountResource destination, AvatarResource destMember, ResolutionContext context)
    {
            if (!string.IsNullOrEmpty(source.Avatar))
            {
                return new AvatarResource
                {
                    Thumbnail = _uriService.GetRouteUri($"{_hostResource.ThumbnailImagePath}{source.Avatar}"),
                    Original = _uriService.GetRouteUri($"{_hostResource.OriginalImagePath}{source.Avatar}"),
                };
            }

            return null;
        }
    #endregion
}