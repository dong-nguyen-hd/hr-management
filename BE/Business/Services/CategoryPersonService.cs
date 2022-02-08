﻿using AutoMapper;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.CategoryPerson;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Business.Services
{
    public class CategoryPersonService : BaseService<CategoryPersonResource, CreateCategoryPersonResource, UpdateCategoryPersonResource, CategoryPerson>, ICategoryPersonService
    {
        #region Constructor
        public CategoryPersonService(ICategoryPersonRepository categoryPersonRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CategoryPersonService> logger,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(categoryPersonRepository, mapper, unitOfWork, logger, responseMessage)
        {
        }
        #endregion
    }
}
