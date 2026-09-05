namespace htmos.model;

public class Member
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BranchID { get; set; }
    public string Phone { get; set; } = string.Empty;
    public Branch Branch { get; set; } = null!;
    public bool IsFirstTimer { get; set; }
}