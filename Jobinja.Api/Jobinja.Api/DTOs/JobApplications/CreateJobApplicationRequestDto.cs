using Microsoft.AspNetCore.Http;

namespace Jobinja.Api.DTOs.JobApplications;

public class CreateJobApplicationRequestDto
{
    public int AgahiId { get; set; }
    public int KarjoId { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IFormFile ResumeFile { get; set; } = null!;
}