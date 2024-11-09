﻿using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class CertificateRepository : BaseRepository<Certificate>, ICertificateRepository
{
    #region Constructor
    public CertificateRepository(CoreContext context) : base(context) { }
    #endregion
}