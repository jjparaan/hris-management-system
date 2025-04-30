using System.Reflection.Emit;
using application.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace application.API.Contexts
{
    internal class ApplicationDbContextHelper
    {
        private readonly ModelBuilder _modelBuilder;
        public ApplicationDbContextHelper(ModelBuilder modelBuilder) {
            _modelBuilder = modelBuilder;
        }

        public void ConfigureEntities()
        {
            ConfigureEmployee();
            ConfigureDepartment();
            ConfigureAttendanceRecord();
            ConfigureLeaveRequest();
            ConfigurePayroll();
            ConfigureUserAccount();
            ConfigureJobTitle();
            ConfigureApplicationLog();
        }
        private void ConfigureEmployee()
        {
            _modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15);

                entity.Property(e => e.HireDate)
                    .IsRequired();

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.JobTitle)
                    .WithMany(j => j.Employees)
                    .HasForeignKey(e => e.JobTitleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasMany(e => e.LeaveRequests)
                    .WithOne(l => l.Employee)
                    .HasForeignKey(l => l.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.AttendanceRecords)
                    .WithOne(a => a.Employee)
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(entity => entity.Payrolls)
                    .WithOne(entity => entity.Employee)
                    .HasForeignKey(entity => entity.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureDepartment()
        {
            _modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(d => d.Name).IsUnique();

                entity.Property(d => d.Description)
                    .HasMaxLength(500);

                entity.HasMany(j => j.Employees)
                    .WithOne(e => e.Department)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureAttendanceRecord()
        {
            _modelBuilder.Entity<AttendanceRecord>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.Employee)
                    .WithMany(e => e.AttendanceRecords)
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.AttendanceDate).IsRequired();

                entity.Property(a => a.CheckInTime)
                    .HasColumnType("time");

                entity.Property(a => a.CheckOutTime)
                    .HasColumnType("time");
            });
        }

        private void ConfigureLeaveRequest()
        {
            _modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.HasOne(l => l.Employee)
                    .WithMany(e => e.LeaveRequests)
                    .HasForeignKey(l => l.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(l => l.StartDate).IsRequired();

                entity.Property(l => l.EndDate)
                    .IsRequired();

                entity.Property(l => l.RequestDate)
                    .IsRequired();

                entity.Property(l => l.Reason)
                    .HasMaxLength(1000);
            });
        }

        private void ConfigurePayroll()
        {
            _modelBuilder.Entity<Payroll>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.Employee)
                    .WithMany(e => e.Payrolls)
                    .HasForeignKey(p => p.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.PayrollDate).IsRequired();

                entity.Property(p => p.BasicSalary)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.Bonus)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.Deductions)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.NetSalary)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        }

        private void ConfigureUserAccount()
        {
            _modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.HasMany(u => u.ApplicationLogs)
                    .WithOne(l => l.UserAccount)
                    .HasForeignKey(l => l.UserAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureJobTitle()
        {
            _modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(j => j.Id);

                entity.Property(j => j.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(j => j.Name).IsUnique();

                entity.Property(j => j.Description)
                    .HasMaxLength(500);

                entity.HasMany(j => j.Employees)
                    .WithOne(e => e.JobTitle)
                    .HasForeignKey(e => e.JobTitleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureApplicationLog()
        {
            _modelBuilder.Entity<ApplicationLog>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.CreatedOn).IsRequired();

                entity.Property(l => l.Level).HasMaxLength(50).IsRequired();

                entity.Property(l => l.Message).IsRequired();

                entity.Property(l => l.Exception).HasMaxLength(2000);

                entity.Property(l => l.Logger).HasMaxLength(255);

                entity.HasOne(p => p.UserAccount)
                    .WithMany(e => e.ApplicationLogs)
                    .HasForeignKey(p => p.UserAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
