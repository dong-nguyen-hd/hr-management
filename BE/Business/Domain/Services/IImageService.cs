using Business.Resources.DTOs.Account;
using Business.Resources.DTOs.Person;
using Business.Results;

namespace Business.Domain.Services;

public interface IImageService
{
    Task<BaseResult<PersonResource>> SaveImagePersonAsync(int personId, Stream imageStream);
    Task<BaseResult<AccountResource>> SaveImageAccountAsync(int accountId, Stream imageStream);
    Task<BaseResult<AccountResource>> ResetAccountAvatarAsync(int id);
    Task<BaseResult<PersonResource>> ResetPersonAvatarAsync(int id);
}