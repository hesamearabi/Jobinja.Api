using Jobinja.Api.Enums;

namespace Jobinja.Api.Entities;

public class Karfarma : BaseEntity
{
    public int UserId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Website { get; set; }
    public string Industry { get; set; } = null!;
    public User User { get; set; } = null!;
}