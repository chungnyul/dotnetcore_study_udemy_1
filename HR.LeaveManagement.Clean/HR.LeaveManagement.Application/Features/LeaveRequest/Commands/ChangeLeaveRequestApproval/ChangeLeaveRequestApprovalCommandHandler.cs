using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender   _emailSender;
        private readonly ILeaveRequestRepository _leaveRequesteRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender, ILeaveRequestRepository leaveRequesteRepository, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _leaveRequesteRepository = leaveRequesteRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequesteRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null) 
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Approved = request.Approved;
            await _leaveRequesteRepository.UpdateAsync(leaveRequest);

            if (request.Approved)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDays -= daysRequested;

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                    Subject = "Leave Request Approval Status Updated"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception)
            {
                // log error
            }

            return Unit.Value;
        }
    }
}
