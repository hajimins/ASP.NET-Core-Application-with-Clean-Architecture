using MediatR;

namespace HR.LeaveManagement.Application.Features.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}