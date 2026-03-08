using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Models.User;

namespace JurysBarManagementSystem.Services
{
    public static class PermissionService
    {
        public static bool CanAccessPayroll(UserRole role)
        {
            return role == UserRole.Admin || role == UserRole.SuperAdmin;
        }

        public static bool CanAccessPayments(UserRole role)
        {
            return role == UserRole.Admin || role == UserRole.SuperAdmin;
        }

        public static bool CanManageProducts(UserRole role)
        {
            return role == UserRole.Admin ||
                   role == UserRole.Manager ||
                   role == UserRole.SuperAdmin;
        }

        public static bool CanProcessSales(UserRole role)
        {
            return role == UserRole.Staff ||
                   role == UserRole.SuperAdmin;
        }
        public static bool CanModifyPayroll(UserRole role)
        {
            return role == UserRole.Admin ||
                   role == UserRole.Manager ||
                   role == UserRole.SuperAdmin;
        }
        public static bool CanManageAccounts(UserRole role)
        {
            return role == UserRole.Admin ||
                   role == UserRole.Manager ||
                   role == UserRole.SuperAdmin;
        }
    }
}