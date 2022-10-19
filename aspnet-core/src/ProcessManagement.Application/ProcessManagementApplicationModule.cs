using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProcessManagement.Authorization;

namespace ProcessManagement
{
    [DependsOn(
        typeof(ProcessManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProcessManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProcessManagementAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProcessManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
