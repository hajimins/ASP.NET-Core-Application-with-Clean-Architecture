using AutoMapper;
using ClassLibrary1.Features.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Domain;

namespace ClassLibrary1.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
    }
}