using AutoMapper;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.Pay;
using Business.Results;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class PayService : BaseService<PayResource, CreatePayResource, UpdatePayResource, Pay>, IPayService
{
    #region Property
    private readonly IPayRepository _payRepository;
    private readonly ITimesheetRepository _timesheetRepository;
    #endregion

    #region Constructor
    public PayService(IPayRepository payRepository,
        ITimesheetRepository timesheetRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(payRepository, mapper, unitOfWork, responseMessage)
    {
        this._payRepository = payRepository;
        this._timesheetRepository = timesheetRepository;
    }
    #endregion

    #region Method
    public async override Task<BaseResult<PayResource>> InsertAsync(CreatePayResource createPayResource)
    {
        try
        {
            var workDayResource = await _timesheetRepository.GetTotalWorkDayAsync(createPayResource.PersonId, createPayResource.Date);
            if (workDayResource is null)
                return new BaseResult<PayResource>(ResponseMessage.Values["Timesheet_NoData"]);

            // Mapping Resource to Pay
            var pay = Mapper.Map<CreatePayResource, Pay>(createPayResource);
            pay.WorkDay = workDayResource.WorkDay;
            pay.TotalWorkDay = workDayResource.TotalWorkDay;

            decimal grossWithoutBonus = (decimal)workDayResource.WorkDay * pay.BaseSalary / (decimal)workDayResource.TotalWorkDay;
            Receivables receivables = new(grossWithoutBonus);

            pay.PIT = receivables.PIT;
            pay.HealthInsurance = receivables.HealthInsurance;
            pay.SocialInsurance = receivables.SocialInsurance;

            await _payRepository.InsertAsync(pay);
            await UnitOfWork.CompleteAsync();

            var payResource = Mapper.Map<Pay, PayResource>(pay);

            return new BaseResult<PayResource>(payResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Pay_Saving_Error"], ex);
        }
    }

    #endregion
}