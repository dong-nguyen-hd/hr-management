using API.Domain.Models;
using API.Resources.DTOs.Pay;

namespace API.Domain.Services;

public interface IPayService : IBaseService<PayResource, CreatePayResource, UpdatePayResource, Pay>
{

}