namespace htmos.model;

public class Branch
{
    public int ID { get; set; }
    public required string Name { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = [];
    public List<Department> Departments { get; set; } = [];
    public BranchAdmin BranchAdmin { get; set; } = null!;
    public List<Worker> Workers { get; set; } = [];
}