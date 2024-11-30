using MediatR;

namespace HR.LeaveManagement.Application.Features.Queries.GetLeaveTypeDetails;

public class GetLeaveTypesQuery
{
    public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
}