using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Data;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Account;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class AccountService : BaseService<AccountResource, CreateAccountResource, UpdateAccountResource, Account>, IAccountService
{
    #region Property
    private readonly IAccountRepository _accountRepository;
    #endregion

    #region Constructor
    public AccountService(IAccountRepository accountRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(accountRepository, mapper, unitOfWork, responseMessage)
    {
        this._accountRepository = accountRepository;
    }
    #endregion

    #region Method
    public override async Task<BaseResponse<AccountResource>> InsertAsync(CreateAccountResource createAccountResource)
    {
        try
        {
            // Validate position name is existent?
            var isExistent = await _accountRepository.ValidateUserNameAsync(createAccountResource.UserName);
            if (!isExistent)
                return new BaseResponse<AccountResource>(ResponseMessage.Values["Account_Existent"]);

            // Mapping Resource to Account
            var tempAccount = Mapper.Map<CreateAccountResource, Account>(createAccountResource);

            await _accountRepository.InsertAsync(tempAccount);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<AccountResource>(Mapper.Map<Account, AccountResource>(tempAccount));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Account_Saving_Error"], ex);
        }
    }

    public async Task<PaginationResponse<IEnumerable<AccountResource>>> GetPaginationAsync(QueryResource pagintation, FilterAccountResource filterResource, eRole? role)
    {
        var paginationAccount = await _accountRepository.GetPaginationAsync(pagintation, filterResource, role);

        // Mapping
        var tempResource = Mapper.Map<IEnumerable<Account>, IEnumerable<AccountResource>>(paginationAccount.records);

        var resource = new PaginationResponse<IEnumerable<AccountResource>>(tempResource);

        // Using extension-method for pagination
        resource.CreatePaginationResponse(pagintation, paginationAccount.total);

        return resource;
    }

    public async Task<BaseResponse<AccountResource>> SelfUpdateAsync(int id, SelfUpdateAccountResource resource)
    {
        try
        {
            var tempAccount = await _accountRepository.GetByIdAsync(id);

            // Update infomation
            Mapper.Map(resource, tempAccount);

            await UnitOfWork.CompleteAsync();

            return new BaseResponse<AccountResource>(Mapper.Map<AccountResource>(tempAccount));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Account_Updating_Error"], ex);
        }
    }

    public async Task<BaseResponse<AccountResource>> UpdatePasswordAsync(int id, UpdatePasswordAccountResource resource)
    {
        try
        {
            // Validate Id is existent?
            var tempAccount = await _accountRepository.GetByIdAsync(id, hasToken: true);
            if (tempAccount is null)
                return new BaseResponse<AccountResource>(ResponseMessage.Values["Account_NoData"]);
            if (!tempAccount.Password.CheckingPassword(resource.OldPassword))
                return new BaseResponse<AccountResource>(ResponseMessage.Values["Account_Password_Error"]);

            // Update infomation
            tempAccount.Password = resource.NewPassword.HashingPassword(Constant.IterationCount);
            tempAccount.LastActivity = DateTime.UtcNow;


            tempAccount?.Tokens?.Clear(); // Remove all token after when change password
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<AccountResource>(Mapper.Map<AccountResource>(tempAccount));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Account_Updating_Error"], ex);
        }
    }

    public async Task<BaseResponse<AccountResource>> RemoveAccountViewerAsync(int id)
    {
        try
        {
            // Validate Id is existent?
            var tempAccount = await _accountRepository.GetByIdAsync(id);
            if (tempAccount is null)
                return new BaseResponse<AccountResource>(ResponseMessage.Values["Account_NoData"]);

            if (tempAccount.Role.Equals(Role.Viewer))
            {
                _accountRepository.Remove(tempAccount);
                await UnitOfWork.CompleteAsync();
                return new BaseResponse<AccountResource>(Mapper.Map<AccountResource>(tempAccount));
            }
            else
            {
                return new BaseResponse<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]);
            }
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Account_Deleting_Error"], ex);
        }
    }
    #endregion
}