using API.Resources.DTOs.Pay;

namespace API.Mapping.Pay;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Pay, PayResource>()
                .ForMember(x => x.SocialInsurancePercent, opt => opt.MapFrom(src => src.SocialInsurance))
                .ForMember(x => x.SocialInsurance, opt => opt.MapFrom(src => CalculateSocialInsurance(src)))
                .ForMember(x => x.HealthInsurancePercent, opt => opt.MapFrom(src => src.HealthInsurance))
                .ForMember(x => x.HealthInsurance, opt => opt.MapFrom(src => CalculateHealthInsurance(src)))
                .ForMember(x => x.PITPercent, opt => opt.MapFrom(src => src.PIT))
                .ForMember(x => x.PIT, opt => opt.MapFrom(src => CalculatePIT(src)))
                .ForMember(x => x.Gross, opt => opt.MapFrom(src => CalculateGross(src)))
                .ForMember(x => x.NET, opt => opt.MapFrom(src => CalculateNET(src)));
        }

    #region Private work
    private static decimal CalculateSocialInsurance(Domain.Models.Pay src)
    {
            decimal grossWithoutBonus = (decimal)src.WorkDay * src.BaseSalary / (decimal)src.TotalWorkDay;

            decimal socialInsurance = grossWithoutBonus / 100 * (decimal)src.SocialInsurance;

            return Math.Round(socialInsurance, 3);
        }

    private static decimal CalculatePIT(Domain.Models.Pay src)
    {
            decimal grossWithoutBonus = (decimal)src.WorkDay * src.BaseSalary / (decimal)src.TotalWorkDay;

            decimal pit = grossWithoutBonus / 100 * (decimal)src.PIT;

            return Math.Round(pit, 3);
        }

    private static decimal CalculateHealthInsurance(Domain.Models.Pay src)
    {
            decimal grossWithoutBonus = (decimal)src.WorkDay * src.BaseSalary / (decimal)src.TotalWorkDay;

            decimal healthInsurance = grossWithoutBonus / 100 * (decimal)src.HealthInsurance;

            return Math.Round(healthInsurance, 3);
        }

    private static decimal CalculateGross(Domain.Models.Pay src)
    {
            decimal grossWithoutBonus = (decimal)src.WorkDay * src.BaseSalary / (decimal)src.TotalWorkDay;

            decimal gross = grossWithoutBonus + src.Bonus + src.Allowance;

            return Math.Round(gross, 3);
        }

    private static decimal CalculateNET(Domain.Models.Pay src)
    {
            decimal grossWithoutBonus = (decimal)src.WorkDay * src.BaseSalary / (decimal)src.TotalWorkDay;

            decimal baseNet = (decimal)((src.PIT + src.SocialInsurance + src.HealthInsurance) / 100) * grossWithoutBonus;
            decimal net = grossWithoutBonus - baseNet + src.Bonus + src.Allowance;

            return Math.Round(net, 3);
        }
    #endregion
}