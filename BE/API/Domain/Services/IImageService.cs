using API.Resources.DTOs.Account;
using API.Resources.DTOs.Person;
using API.Results;

namespace API.Domain.Services;

public interface IImageService
{
    Task<BaseResult<PersonResource>> SaveImagePersonAsync(int personId, Stream imageStream);
    Task<BaseResult<AccountResource>> SaveImageAccountAsync(int accountId, Stream imageStream);
    Task<BaseResult<AccountResource>> ResetAccountAvatarAsync(int id);
    Task<BaseResult<PersonResource>> ResetPersonAvatarAsync(int id);
}