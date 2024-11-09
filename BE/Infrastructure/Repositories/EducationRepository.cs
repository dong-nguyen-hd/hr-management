using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class EducationRepository : BaseRepository<Education>, IEducationRepository
{
    #region Constructor
    public EducationRepository(CoreContext context) : base(context) { }
    #endregion
}