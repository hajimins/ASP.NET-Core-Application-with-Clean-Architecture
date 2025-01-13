namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public abstract class BaseLeaveRequest
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int LeaveTypeId { get; set; }
}