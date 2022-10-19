using Abp.Authorization;
using ProcessManagement.Authorization.Roles;
using ProcessManagement.Authorization.Users;

namespace ProcessManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
