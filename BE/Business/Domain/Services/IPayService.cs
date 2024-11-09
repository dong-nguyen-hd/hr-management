using Business.Domain.Models;
using Business.Resources.DTOs.Pay;

namespace Business.Domain.Services;

public interface IPayService : IBaseService<PayResource, CreatePayResource, UpdatePayResource, Pay>
{

}