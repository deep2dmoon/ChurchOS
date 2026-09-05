using htmos.model;
using Microsoft.EntityFrameworkCore;
using producer.model;

namespace htmos.data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<BranchAdmin> BranchAdmins => Set<BranchAdmin>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Worker> Workers => Set<Worker>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Branch>().HasData(new Branch { ID = 1, Name = "HQTRS" });

        modelBuilder.Entity<User>().HasData(new User
        {
            Name = "Harvest Tabernacle Ministry",
            RoleID = 1,
            Email = "htm@hq.go",
            ID = 1,
            Password = "pass-01",
        });

        modelBuilder.Entity<Role>().HasData(new Role { ID = 1, Name = "Admin" }, new Role { ID = 2, Name = "BranchAdmin" }, new Role { ID = 3, Name = "Member" });
        modelBuilder.Entity<Permission>().HasData(new Permission { ID = 1, Name = "Read" }, new Permission { ID = 2, Name = "Write" }, new Permission { ID = 3, Name = "Branch.Write" });

        modelBuilder.Entity<RolePermission>().HasData(
         new RolePermission { RoleID = 1, PermissionID = 1 },
         new RolePermission { RoleID = 1, PermissionID = 2 },
         new RolePermission { RoleID = 1, PermissionID = 3 },
         new RolePermission { RoleID = 2, PermissionID = 1 },
         new RolePermission { RoleID = 2, PermissionID = 2 },
         new RolePermission { RoleID = 3, PermissionID = 1 });

        modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleID, rp.PermissionID }); // giving the RolePermission table a composite key for every rows 
        modelBuilder.Entity<BranchAdmin>().HasKey(ba => new { ba.BranchID, ba.UserID });
        modelBuilder.Entity<BranchAdmin>().HasOne(ba => ba.User).WithMany().HasForeignKey(ba => ba.UserID).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<BranchAdmin>().HasOne(ba => ba.Branch).WithMany().HasForeignKey(ba => ba.BranchID).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Member>().HasOne(m => m.Branch).WithMany().HasForeignKey(m => m.BranchID).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Worker>().HasKey(w => new { w.DepartmentID, w.MemberID });

    }

}