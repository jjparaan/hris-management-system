using application.API.Repositories.Interfaces;
using application.API.Services.Interfaces.ApplicationLogs;
using application.API.Services.Models;
using application.ApplicationLayer.DTOs;
using application.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace application.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly ILogService _logger;
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;

        public JobTitleController(ILogService logger, IJobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _logger = logger;
            _jobTitleRepository = jobTitleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/getjobtitles")]
        public async Task<ApplicationHttpResponse<List<JobTitle>>> GetJobTitles()
        {
            await _logger.LogInformationAsync("Fetching all Job titles", "GetJobTitles");
            List<JobTitle> jobTitles = await _jobTitleRepository.GetJobTitlesAsync();

            return new ApplicationHttpResponse<List<JobTitle>>
            {
                Message = $"Job titles successfully fetched",
                Data = jobTitles
            };
        }

        [HttpGet]
        [Route("/getjobtitle/{id}")]
        public async Task<ApplicationHttpResponse<JobTitle>> GetJobTitle([FromRoute] int id)
        {
            JobTitle jobTitle = await _jobTitleRepository.GetJobTitleByIdAsync(id);

            #region Validations
            if (jobTitle == null)
            {
                await _logger.LogInformationAsync($"Job title with id {id} does not exist", "GetJobTitle");
                return new ApplicationHttpResponse<JobTitle>
                {
                    Status = HttpStatusCode.NotFound,
                    Message = $"Job title with id {id} does not exist",
                    Data = null
                };
            }
            #endregion

            return new ApplicationHttpResponse<JobTitle>
            {
                Message = $"Job title successfully fetched",
                Data = jobTitle
            };
        }

        [HttpPost]
        [Route("/insertjobtitle")]
        [Consumes("application/json")]
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
