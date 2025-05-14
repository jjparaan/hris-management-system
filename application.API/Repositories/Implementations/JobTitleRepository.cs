using application.API.Contexts;
using application.API.Repositories.Interfaces;
using application.API.Services.Interfaces.ApplicationLogs;
using application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace application.API.Repositories.Implementations
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
