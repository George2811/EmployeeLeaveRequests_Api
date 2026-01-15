using Microsoft.EntityFrameworkCore;
using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Extensions;

namespace EmployeeLeaveRequests.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(s => s.Id);
            builder.Entity<Employee>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Entity<Employee>().Property(s => s.Email).IsRequired().HasMaxLength(200);
            builder.Entity<Employee>().Property(s => s.Role).IsRequired().HasMaxLength(200);

            builder.Entity<LeaveRequest>().ToTable("LeaveRequests");
            builder.Entity<LeaveRequest>().HasKey(s => s.Id);
            builder.Entity<LeaveRequest>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<LeaveRequest>().Property(s => s.EmployeeId).IsRequired().HasMaxLength(200);
            builder.Entity<LeaveRequest>().Property(s => s.StartDate).IsRequired();
            builder.Entity<LeaveRequest>().Property(s => s.EndDate).IsRequired();
            builder.Entity<LeaveRequest>().Property(s => s.Status).IsRequired();
            builder.Entity<LeaveRequest>().Property(s => s.Reason).IsRequired();

            //builder.Entity<LeaveRequest>()
            //.HasOne(lr => lr.EmployeeId)          // Una solicitud tiene un Empleado
            //.WithMany(e => e.LeaveRequests)     // Un Empleado tiene muchas solicitudes
            //.HasForeignKey(lr => lr.EmployeeId) // Clave foránea
            //.OnDelete(DeleteBehavior.Cascade);

            // Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
