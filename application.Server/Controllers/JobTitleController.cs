using application.Server.Entities;
using application.Server.Entities.DTOs;
using application.Server.Repositories.Implementations;
using application.Server.Services.Interfaces.ApplicationLogs;
using application.Server.Services.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace application.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly ILogService _logger;
        private readonly JobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;

        public JobTitleController(ILogService logger, JobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _logger = logger;
            _jobTitleRepository = jobTitleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/getjobtitles")]
        public async Task<List<JobTitle>> GetJobTitles()
        {
            await _logger.LogInformationAsync("Fetching all Job titles", "GetJobTitles");
            return await _jobTitleRepository.GetJobTitlesAsync();
        }

        [HttpPost]
        [Route("/insertjobtitle")]
        public async Task<ApplicationHttpResponse<JobTitle>> InsertJobTitle([FromBody] JobTitleDTO jobTitleDTO)
        {
            // TODO: Create a reusable validation handler for request body
            #region Validations
            if (jobTitleDTO == null)
            {
                await _logger.LogInformationAsync($"Server could not process your request. Parameter is empty", "InsertJobTitle");

                return new ApplicationHttpResponse<JobTitle>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"ParameterServer could not process your request. Parameter is empty",
                    Data = null
                };
            }

            List<JobTitle> jobTitles = await _jobTitleRepository.GetJobTitlesAsync();
            if (jobTitles.Any(x => x.Name.Equals(jobTitleDTO.Name, StringComparison.OrdinalIgnoreCase)))
            {
                await _logger.LogInformationAsync($"Server could not process your request. Job title already exists", "InsertJobTitle");
                return new ApplicationHttpResponse<JobTitle>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"Server could not process your request. Job title already exists",
                    Data = null
                };
            }

            if (string.IsNullOrWhiteSpace(jobTitleDTO.Name))
            {
                await _logger.LogInformationAsync($"Server could not process your request. Name is required", "InsertJobTitle");

                return new ApplicationHttpResponse<JobTitle>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"Server could not process your request. Name is required",
                    Data = null
                };
            }

            int descriptionCharLimit = 500;
            if (jobTitleDTO.Description.Length > descriptionCharLimit)
            {
                await _logger.LogInformationAsync($"Server could not process your request. Description has reached its maximum content of {descriptionCharLimit}", "InsertJobTitle");

                return new ApplicationHttpResponse<JobTitle>
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"Server could not process your request. Description has reached its maximum content of {descriptionCharLimit}",
                    Data = null
                };
            }
            #endregion

            JobTitle jobTitle = _mapper.Map<JobTitleDTO, JobTitle>(jobTitleDTO);
            await _jobTitleRepository.InsertJobTitleAsync(jobTitle);
            await _logger.LogInformationAsync($"Job title successfully inserted", "InsertJobTitle");
            return new ApplicationHttpResponse<JobTitle>
            {
                Message = $"Job title successfully inserted",
                Data = jobTitle
            };
        }
    }
}
