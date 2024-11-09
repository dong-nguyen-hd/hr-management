using API.Resources.DTOs.Authentication;

namespace API.Mapping.Token;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.RefreshToken, TokenResource>()
                .ForMember(x => x.AccessToken, opt => opt.Ignore());
        }
}