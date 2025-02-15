using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveAllocationCommandHandler(IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository 
            leaveTypeRepository)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation request", validationResult);
        
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
        await _leaveAllocationRepository.CreateAsync(leaveAllocation);
        return Unit.Value;
    }
}