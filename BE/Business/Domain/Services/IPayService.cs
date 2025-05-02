using Business.Domain.Models;
using Business.Resources.Pay;

namespace Business.Domain.Services;

public interface IPayService : IBaseService<PayResource, CreatePayResource, UpdatePayResource, Pay>
{

}