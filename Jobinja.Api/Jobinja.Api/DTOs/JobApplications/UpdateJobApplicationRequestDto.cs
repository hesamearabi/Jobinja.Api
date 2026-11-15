
namespace Jobinja.Api.DTOs.JobApplications;

public class UpdateJobApplicationRequestDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IFormFile? ResumeFile { get; set; }
}