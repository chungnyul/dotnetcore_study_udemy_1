using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            //
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            // LeaveAllocationDto : Id, NumberOfDays, LeaveType, LeaveTypeId, Period
            // LeaveAllocation : NumberOfDays, LeaveType, LeaveTypeId, Period, EmployeeId

            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
            // LeaveAllocation : NumberOfDays, LeaveType, LeaveTypeId, Period, EmployeeId
            // LeaveAllocationDetailsDto : Id, NumberOfDays, LeaveType, LeaveTypeId, Period

            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            // CreateLeaveAllocationCommand : LeaveTypeId
            // LeaveAllocation : NumberOfDays, LeaveType, LeaveTypeId, Period, EmployeeId
        }
    }
}
