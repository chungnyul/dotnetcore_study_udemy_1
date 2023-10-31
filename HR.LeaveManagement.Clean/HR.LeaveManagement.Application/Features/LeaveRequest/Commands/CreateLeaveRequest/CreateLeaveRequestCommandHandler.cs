using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequesteRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequesteRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequesteRepository = leaveRequesteRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            //var employeeId = _userService.UserId;

            //// Check on employee's allocation
            //var allocation = await _leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);

            //// if allocations aren't enough, return validation error with message
            //if (allocation is null)
            //{
            //    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId),
            //        "You do not have any allocations for this leave type."));
            //    throw new BadRequestException("Invalid Leave Request", validationResult);
            //}

            //int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
            //if (daysRequested > allocation.NumberOfDays)
            //{
            //    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
            //        nameof(request.EndDate), "You do not have enough days for this request"));
            //    throw new BadRequestException("Invalid Leave Request", validationResult);
            //}

            //var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            //leaveRequest.RequestingEmployeeId = employeeId;
            //leaveRequest.DateRequested = DateTime.Now;
            //await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"
                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception)
            {
                //// Log or handle error,
            }


            return Unit.Value;
        }
    }
}
