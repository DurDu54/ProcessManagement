using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProcessManagement.Authorization.Roles;
using ProcessManagement.Authorization.Users;
using ProcessManagement.MultiTenancy;

namespace ProcessManagement.EntityFrameworkCore
{
    public class ProcessManagementDbContext : AbpZeroDbContext<Tenant, Role, User, ProcessManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ProcessManagementDbContext(DbContextOptions<ProcessManagementDbContext> options)
            : base(options)
        {
        }
    }
}
