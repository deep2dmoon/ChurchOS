namespace htmos.model;

public class Permission
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions = [];
}