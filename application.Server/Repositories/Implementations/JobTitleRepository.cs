using application.Server.Contexts;
using application.Server.Entities;
using application.Server.Repositories.Interfaces;
using application.Server.Services.Interfaces.ApplicationLogs;
using Microsoft.EntityFrameworkCore;

namespace application.Server.Repositories.Implementations
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly ApplicationDbContext _context;

        public JobTitleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobTitle>> GetJobTitlesAsync() => await _context.JobTitles.ToListAsync();

        public async Task<JobTitle> GetJobTitleByIdAsync(int id) => await _context.JobTitles.Include(j => j.Employees).FirstOrDefaultAsync(j => j.Id == id);

        public async Task InsertJobTitleAsync(JobTitle jobTitle)
        {
            await _context.JobTitles.AddAsync(jobTitle);
            await _context.SaveChangesAsync();
        }
    }
}
