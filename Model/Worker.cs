namespace htmos.model;

public class Worker
{
    public int ID { get; set; }
    public int DepartmentID { get; set; }
    public Department Department { get; set; } = null!;
    public int MemberID { get; set; }
    public List<Member> Members { get; set; } = [];
}