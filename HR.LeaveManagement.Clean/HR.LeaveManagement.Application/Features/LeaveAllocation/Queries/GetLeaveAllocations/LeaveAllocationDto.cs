using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDto
    {
        public int Id { get; set; } 
        public int NuberOfDays {  get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypdId { get; set; }
        public int Period { get; set; }
    }
}
