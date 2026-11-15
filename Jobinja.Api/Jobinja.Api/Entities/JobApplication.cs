using Jobinja.Api.Enums;

namespace Jobinja.Api.Entities;

public class JobApplication : BaseEntity
{
    public int AgahiId { get; set; }
    public int KarjoId { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string ResumePath { get; set; } = null!;
    public Agahi Agahi { get; set; } = null!;
    public Karjo Karjo { get; set; } = null!;
}