using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using TestWebPenjualan.Domain.Entities;

namespace TestWebPenjualan.Infrastructure.Persistance.EntityFramework;

public class UserManagementDbContext : IdentityDbContext<User>
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
    {
    }
}
