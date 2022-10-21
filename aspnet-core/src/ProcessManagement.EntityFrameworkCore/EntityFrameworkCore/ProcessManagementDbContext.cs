using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProcessManagement.Authorization.Roles;
using ProcessManagement.Authorization.Users;
using ProcessManagement.MultiTenancy;
using ProcessManagement.Customers;
using ProcessManagement.Managers;
using ProcessManagement.Developers;
using ProcessManagement.Projects;
using ProcessManagement.Professionlar;
using ProcessManagement.Missions;

namespace ProcessManagement.EntityFrameworkCore
{
    public class ProcessManagementDbContext : AbpZeroDbContext<Tenant, Role, User, ProcessManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Profession> Professions{ get; set; }
        public DbSet<Mission> Missions{ get; set; }
        public DbSet<Commit> Commits { get; set; }

        public ProcessManagementDbContext(DbContextOptions<ProcessManagementDbContext> options)
            : base(options)
        {
        }
    }
}
