﻿using AutoMapper;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Certificate;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Business.Services
{
    public class CertificateService : BaseService<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>, ICertificateService
    {
        #region Constructor
        public CertificateService(ICertificateRepository certificateRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CertificateService> logger,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(certificateRepository, mapper, unitOfWork, logger, responseMessage)
        {
        }
        #endregion
    }
}
