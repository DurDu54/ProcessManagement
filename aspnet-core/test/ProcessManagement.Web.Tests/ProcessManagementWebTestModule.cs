using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProcessManagement.EntityFrameworkCore;
using ProcessManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ProcessManagement.Web.Tests
{
    [DependsOn(
        typeof(ProcessManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ProcessManagementWebTestModule : AbpModule
    {
        public ProcessManagementWebTestModule(ProcessManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProcessManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ProcessManagementWebMvcModule).Assembly);
        }
    }
}