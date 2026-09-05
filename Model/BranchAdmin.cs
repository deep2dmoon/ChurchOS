using producer.model;

namespace htmos.model;

public class BranchAdmin
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public User User { get; set; } = null!;
    public int BranchID { get; set; }
    public Branch Branch { get; set; } = null!;
}