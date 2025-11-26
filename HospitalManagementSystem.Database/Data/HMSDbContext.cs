using HospitalManagementSystem.Models.DatabaseEntity.Department;
using HospitalManagementSystem.Models.DatabaseEntity.Patient;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Database.Data;

public class HMSDbContext : IdentityDbContext<ApplicationUser>
{
    public HMSDbContext(DbContextOptions<HMSDbContext> options) : base(options)
    {

    }
    //db table
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}