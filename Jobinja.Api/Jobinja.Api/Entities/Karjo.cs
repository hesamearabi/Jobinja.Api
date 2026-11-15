using Jobinja.Api.Enums;
using Microsoft.AspNetCore.Builder;
using Jobinja.Api.Enum;
namespace Jobinja.Api.Entities;

public class Karjo : BaseEntity
{
    public int UserId { get; set; }
    public string JobTitle { get; set; } = null!;
    public Province Province { get; set; }
    public ContractType ContractType { get; set; }
    public List<JobCategory> JobCategories { get; set; } = [];
    public List<Skill> Skills { get; set; } = [];
    public string? ResumePath { get; set; }
    public User User { get; set; } = null!;
    public List<JobApplication> JobApplications { get; set; } = [];

}