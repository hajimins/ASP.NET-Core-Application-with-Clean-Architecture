using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler :
    IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;  
    private readonly IEmailSender _emailSender;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;    
    
    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        this._emailSender = emailSender;
        this._mapper = mapper;
    }
    
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        var email = new EmailMessage
        {
            To = string.Empty,
            Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                   $"has been updated.",
            Subject = "Leave Request Approval Status Updated"
        };

        await _emailSender.SendEmail(email);
        
        return Unit.Value;
    }
}