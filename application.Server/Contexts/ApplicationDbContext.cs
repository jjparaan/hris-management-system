using application.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace application.Server.Contexts
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationDbContextHelper helper = new ApplicationDbContextHelper(modelBuilder);
            helper.ConfigureEntities();
        }
    }
}
