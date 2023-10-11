using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }


        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve domain entity object;
            var leaveTypeToDelete = _leaveTypeRepository.GetByIdAsync(request.Id);

            // verify that record exist;


            // remove from database;

            // 원래 강좌에 있는 내용
            //await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete); 

            // 변환 에러에 따른 수정한 내용
            var data = _mapper.Map<Domain.LeaveType>(leaveTypeToDelete);
            await _leaveTypeRepository.DeleteAsync(data);


            // ruturn record id;
            return Unit.Value;

        }
    }
}
