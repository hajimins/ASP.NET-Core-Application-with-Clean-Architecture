using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
     public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
          IConfiguration configuration)
     { 
        //  services.AddDbContext<HrDatabaseContext>(options =>
          //options.UseNpgsql(configuration.GetConnectionString("HrDatabaseConnectionString")));

          services.AddDbContext<HrDatabaseContext>(options =>
               options
                    .UseNpgsql(configuration.GetConnectionString("HrDatabaseConnectionString"))
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

          services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
          services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
          services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
          return services;
     }
}