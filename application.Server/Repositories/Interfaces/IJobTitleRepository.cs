using application.API.Entities;

namespace application.API.Repositories.Interfaces
{
    public interface IJobTitleRepository
    {
        public Task<List<JobTitle>> GetJobTitlesAsync();
        public Task<JobTitle> GetJobTitleByIdAsync(int id);

        public Task InsertJobTitleAsync(JobTitle jobTitle);
    }
}
