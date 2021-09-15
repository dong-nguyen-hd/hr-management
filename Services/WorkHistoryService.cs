﻿#nullable enable
using AutoMapper;
using HR_Management.Domain.Models;
using HR_Management.Domain.Repositories;
using HR_Management.Domain.Services;
using HR_Management.Domain.Services.Communication;
using HR_Management.Resources;
using HR_Management.Resources.WorkHistory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management.Services
{
    public class WorkHistoryService : ResponseMessageService, IWorkHistoryService
    {
        private readonly IWorkHistoryRepository _workHistoryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WorkHistoryService(IWorkHistoryRepository workHistoryRepository,
            IPersonRepository personRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IOptionsSnapshot<ResponseMessage> responseMessage) : base(responseMessage)
        {
            this._workHistoryRepository = workHistoryRepository;
            this._personRepository = personRepository;
            this._mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkHistoryResponse<IEnumerable<WorkHistoryResource>>> ListAsync(int personId)
        {
            // Validate person is existent?
            var tempPerson = await _personRepository.FindByIdAsync(personId);
            if (tempPerson is null)
                return new WorkHistoryResponse<IEnumerable<WorkHistoryResource>>(ResponseMessage.Values["Person_Id_NoData"]);
            // Get list record from DB
            var tempWorkHistory = await _workHistoryRepository.ListAsync(personId);
            // Mapping Project to Resource
            var resource = _mapper.Map<IEnumerable<WorkHistory>, IEnumerable<WorkHistoryResource>>(tempWorkHistory);

            return new WorkHistoryResponse<IEnumerable<WorkHistoryResource>>(resource);
        }

        public async Task<WorkHistoryResponse<WorkHistoryResource>> CreateAsync(CreateWorkHistoryResource createWorkHistoryResource)
        {
            // Validate person is existent?
            var tempPerson = await _personRepository.FindByIdAsync(createWorkHistoryResource.PersonId);
            if (tempPerson is null)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["Person_Id_NoData"]);

            // Mapping Resource to WorkHistory
            var workHistory = _mapper.Map<CreateWorkHistoryResource, WorkHistory>(createWorkHistoryResource);
            workHistory.OrderIndex = FindMaximum(workHistory.PersonId);

            try
            {
                await _workHistoryRepository.AddAsync(workHistory);
                await _unitOfWork.CompleteAsync();
                // Mapping
                var resource = _mapper.Map<WorkHistory, WorkHistoryResource>(workHistory);

                return new WorkHistoryResponse<WorkHistoryResource>(resource);
            }
            catch (Exception ex)
            {
                return new WorkHistoryResponse<WorkHistoryResource>($"{ResponseMessage.Values["WorkHistory_Saving_Error"]}: {ex.Message}");
            }
        }

        /// <summary>
        /// Find maximum value of OrderIndex
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int FindMaximum(int id)
        {
            int maximumOrderIndex = _workHistoryRepository.MaximumOrderIndex(id);
            maximumOrderIndex = (maximumOrderIndex <= 0) ? 1 : maximumOrderIndex + 1;

            return maximumOrderIndex;
        }


        public async Task<WorkHistoryResponse<WorkHistoryResource>> UpdateAsync(int id, UpdateWorkHistoryResource updateWorkHistoryResource)
        {
            // Validate Id is existent?
            var tempWorkHistory = await _workHistoryRepository.FindByIdAsync(id);
            if (tempWorkHistory is null)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["WorkHistory_NoData"]);
            // Updating
            _mapper.Map(updateWorkHistoryResource, tempWorkHistory);

            try
            {
                await _unitOfWork.CompleteAsync();
                // Mapping
                var resource = _mapper.Map<WorkHistory, WorkHistoryResource>(tempWorkHistory);

                return new WorkHistoryResponse<WorkHistoryResource>(resource);
            }
            catch (Exception ex)
            {
                return new WorkHistoryResponse<WorkHistoryResource>($"{ResponseMessage.Values["WorkHistory_Updating_Error"]}: {ex.Message}");
            }
        }

        public async Task<WorkHistoryResponse<WorkHistoryResource>> DeleteAsync(int id)
        {
            // Validate Id is existent?
            var tempWorkHistory = await _workHistoryRepository.FindByIdAsync(id);
            if (tempWorkHistory is null)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["WorkHistory_NoData"]);
            // Change property Status: true -> false
            tempWorkHistory.Status = false;

            try
            {
                await _unitOfWork.CompleteAsync();
                // Mapping
                var resource = _mapper.Map<WorkHistory, WorkHistoryResource>(tempWorkHistory);

                return new WorkHistoryResponse<WorkHistoryResource>(resource);
            }
            catch (Exception ex)
            {
                return new WorkHistoryResponse<WorkHistoryResource>($"{ResponseMessage.Values["WorkHistory_Deleting_Error"]}: {ex.Message}");
            }
        }

        public async Task<WorkHistoryResponse<WorkHistoryResource>> SwapAsync(SwapResource obj)
        {
            // Validate Id duplicate
            if (obj.CurrentId == obj.TurnedId)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["Swap_Id_Invalid"]);
            // Validate Id is existent?
            var currentWorkHistory = await _workHistoryRepository.FindByIdAsync(obj.CurrentId);
            var turnedWorkHistory = await _workHistoryRepository.FindByIdAsync(obj.TurnedId);
            if (currentWorkHistory is null || turnedWorkHistory is null)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["WorkHistory_NoData"]);
            if (currentWorkHistory.PersonId != turnedWorkHistory.PersonId)
                return new WorkHistoryResponse<WorkHistoryResource>(ResponseMessage.Values["Swap_Id_Invalid"]);
            // Swap property OrderIndex
            int tempOrderIndex = -1;
            tempOrderIndex = currentWorkHistory.OrderIndex;
            currentWorkHistory.OrderIndex = turnedWorkHistory.OrderIndex;
            turnedWorkHistory.OrderIndex = tempOrderIndex;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new WorkHistoryResponse<WorkHistoryResource>(new WorkHistoryResource());
            }
            catch (Exception ex)
            {
                return new WorkHistoryResponse<WorkHistoryResource>($"{ResponseMessage.Values["WorkHistory_Swapping_Error"]}: {ex.Message}");
            }
        }
    }
}
