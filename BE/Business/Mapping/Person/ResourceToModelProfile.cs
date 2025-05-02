using AutoMapper;
using Business.Data;
using Business.Extensions;
using Business.Resources.Person;

namespace Business.Mapping.Person;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreatePersonResource, Domain.Models.Person>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.FirstName.RemoveSpaceCharacter()))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName.RemoveSpaceCharacter()))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email.RemoveSpaceCharacter()))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description.RemoveSpaceCharacter()))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone.RemoveSpaceCharacter()))
                .ForMember(x => x.CategoryPersons, opt => opt.MapFrom(src => src.CategoryPersonResource))
                .ForMember(x => x.Certificates, opt => opt.MapFrom(src => src.CertificateResource))
                .ForMember(x => x.Educations, opt => opt.MapFrom(src => src.EducationResource))
                .ForMember(x => x.Projects, opt => opt.MapFrom(src => src.ProjectResource))
                .ForMember(x => x.WorkHistories, opt => opt.MapFrom(src => src.WorkHistoryResource))
                .ForMember(x => x.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex.RemoveDuplicate().ConcatenateWithComma()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.StaffId, opt => opt.MapFrom(src => ComputeStaffId()))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(src => Constant.DefaultAvatar)) // Use default image for new person.
                .ForMember(x => x.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex.RemoveDuplicate().ConcatenateWithComma()));

            CreateMap<UpdatePersonResource, Domain.Models.Person>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.FirstName.RemoveSpaceCharacter()))
                .ForMember(x => x.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex.RemoveDuplicate().ConcatenateWithComma()))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName.RemoveSpaceCharacter()))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email.RemoveSpaceCharacter()))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description.RemoveSpaceCharacter()))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone.RemoveSpaceCharacter()));
        }

    /// <summary>
    /// You should rewrite this method, rely on Id of person in DB
    /// </summary>
    /// <returns></returns>
    private string ComputeStaffId()
    {
            DateTime tempDate = DateTime.Now;
            string tempMonth = tempDate.Month < 10 ? $"0{tempDate.Month}" : $"{tempDate.Month}";
            string tempDay = tempDate.Day < 10 ? $"0{tempDate.Day}" : $"{tempDate.Day}";

            Random tempRnd = new Random();
            int lastDigit = tempRnd.Next(1000, 9999);

            return string.Format($"{tempDate.Year}{tempMonth}{tempDay}{lastDigit}");
        }
}