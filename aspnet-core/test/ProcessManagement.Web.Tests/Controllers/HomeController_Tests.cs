using System.Threading.Tasks;
using ProcessManagement.Models.TokenAuth;
using ProcessManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace ProcessManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: ProcessManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}