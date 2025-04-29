using application.Server.Entities;

namespace application.Server.Repositories.Interfaces
{
    public interface IJobTitleRepository
    {
        public Task<List<JobTitle>> GetJobTitlesAsync();
        public Task<JobTitle> GetJobTitleAsync(JobTitle jobTitle);

        public Task InsertJobTitleAsync(JobTitle jobTitle);
    }
}
