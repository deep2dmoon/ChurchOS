namespace htmos.model;

public class RolePermission
{
    public int RoleID { get; set; }
    public Role Role { get; set; } = null!;
    public int PermissionID { get; set; }
    public Permission Permission { get; set; } = null!;
}