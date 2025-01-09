using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries;

public class GetLeaveAllocationDetailQuery : IRequest<LeaveAllocationDetailsDto>
{
    public int Id { get; set; }
}