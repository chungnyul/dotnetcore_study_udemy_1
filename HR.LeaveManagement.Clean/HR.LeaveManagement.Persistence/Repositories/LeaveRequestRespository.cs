using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRespository : GenericRepository<LeaveRequest>, ILeaveRequesteRepository
    {
        public LeaveRequestRespository(HrDatabaseContext context) : base(context)
        {
        }
    }
}
