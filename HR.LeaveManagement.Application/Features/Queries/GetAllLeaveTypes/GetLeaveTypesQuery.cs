using MediatR;

namespace HR.LeaveManagement.Application.Features.Queries.GetAllLeaveTypes;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;