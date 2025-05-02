using AutoMapper;
using Business.Resources.Authentication;

namespace Business.Mapping.Token;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Token, TokenResource>()
                .ForMember(x => x.AccessToken, opt => opt.Ignore());
        }
}