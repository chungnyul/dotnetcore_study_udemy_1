using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            // LeaveTypeDto는 dto객체 : Id, Name, DefaultDays
            // LeaveType은 domani 객체 : Name, DefaultDays

            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            // LeaveType은 domain 객체: Name, DefaultDays
            // LeaveTypeDetailsDto는 dto객체 : Id, Name, DefaultDays, DateCreated, DateModified
        }
    }
}
 