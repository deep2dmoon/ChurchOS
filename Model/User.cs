using htmos.model;

namespace producer.model;

public class User
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleID { get; set; }
    public Role Role { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
  
}