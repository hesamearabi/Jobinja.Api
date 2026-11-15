using Jobinja.Api.Enums;

namespace Jobinja.Api.DTOs.JobApplications;

public class GetJobApplicationResponseDto
{
    public int Id { get; set; }
    public int AgahiId { get; set; }
    public string AgahiTitle { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string ResumePath { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}