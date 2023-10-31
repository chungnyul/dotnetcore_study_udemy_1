using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequesteRepository;
        private readonly IMapper _mapper;
        
        private GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequesteRepository, IMapper mapper)
        {
            _leaveRequesteRepository = leaveRequesteRepository;
            _mapper = mapper;
        }


        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            // Check if it is logged in employee
            if (request.IsLoggedInUser)
            {
                //var userId = _userService.UserId;
                //leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                //var employee = await _userService.GetEmployee(userId);
                //requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                //foreach (var req in requests)
                //{
                //    req.Employee = employee;
                //}
            }
            else
            {
                leaveRequests = await _leaveRequesteRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    //  req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;

        }
    }
}
