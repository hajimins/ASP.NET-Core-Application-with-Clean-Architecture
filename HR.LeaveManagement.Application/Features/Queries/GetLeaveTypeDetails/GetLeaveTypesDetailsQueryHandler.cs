using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.Queries.GetLeaveTypeDetails;

public class GetLeaveTypesDetailsQueryHandler : IRequestHandler<GetLeaveTypesQuery.GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypesDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        this._mapper = mapper;
        this._leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypesQuery.GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var leaveTypes =  await _leaveTypeRepository.GetByIdAsync(request.Id);
        
        // Convert data objects to DTO objects
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypes);
        
        // Return list of DTO objects
        return data;
    }
}

