using API.Extensions;
using API.Resources.DTOs.Account;
using API.Resources.Enums;

namespace API.Mapping.Account;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateAccountResource, Domain.Models.Account>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.UserName.ToLower()))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password.ToLower().HashingPassword(Constant.IterationCount)))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(src => Constant.DefaultAvatar)) // Use default image for new account.
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.LastActivity, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => GetRole(src.Role)));

            CreateMap<UpdateAccountResource, Domain.Models.Account>()
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password.HashingPassword(Constant.IterationCount)))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.LastActivity, opt => opt.Ignore())
                .ForMember(x => x.Role, opt => opt.MapFrom(src => GetRole(src.Role)));

            CreateMap<SelfUpdateAccountResource, Domain.Models.Account>()
                .ForMember(x => x.LastActivity, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));
        }

    #region Private work
    private static string GetRole(int @enum)
    {
            eRole roleEnum = (eRole)@enum;

            return roleEnum.ToDescriptionString();
        }
    #endregion
}