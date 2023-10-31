using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequesteRepository;
        private readonly IMapper _mapper;
        //private readonly IUserService _userService;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequesteRepository, IMapper mapper)
        {
            _leaveRequesteRepository = leaveRequesteRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequesteRepository.GetLeaveRequestWithDetails(request.Id));

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            //leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

            return leaveRequest;
        }
    }
}
