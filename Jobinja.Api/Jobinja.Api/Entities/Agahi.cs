using Jobinja.Api.Enum;
using Jobinja.Api.Enums;

namespace Jobinja.Api.Entities;

public class Agahi : BaseEntity
{
    public int KarfarmaId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public JobCategory JobCategory { get; set; }
    public Province Province { get; set; }
    public ContractType ContractType { get; set; }
    public List<Skill> RequiredSkills { get; set; } = [];
    public decimal? Salary { get; set; }
    public bool IsActive { get; set; } = true;
    public Karfarma Karfarma { get; set; } = null!;
    public List<JobApplication> JobApplications { get; set; } = [];
}