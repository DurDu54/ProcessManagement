using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ProcessManagement.EntityFrameworkCore
{
    public static class ProcessManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProcessManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ProcessManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
