using Jobinja.Api.Entities;
using Jobinja.Api.Enums;
public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserType UserType { get; set; }
}