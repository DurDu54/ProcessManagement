using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProcessManagement.Configuration;

namespace ProcessManagement.Web.Host.Startup
{
    [DependsOn(
       typeof(ProcessManagementWebCoreModule))]
    public class ProcessManagementWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ProcessManagementWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProcessManagementWebHostModule).GetAssembly());
        }
    }
}
