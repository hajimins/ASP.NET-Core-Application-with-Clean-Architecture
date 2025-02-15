using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;
    
    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, 
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        this._emailSender = emailSender;
        this._mapper = mapper;
        this._appLogger = appLogger;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        
        if(leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request);
        
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);
        
        _mapper.Map(request, leaveRequest);
        
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been updated successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch(Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
        }
        
        return Unit.Value;
    }
}