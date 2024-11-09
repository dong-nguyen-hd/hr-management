using AutoMapper;
using Business.Resources.DTOs.Authentication;

namespace Business.Mapping.Token;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.RefreshToken, TokenResource>()
                .ForMember(x => x.AccessToken, opt => opt.Ignore());
        }
}