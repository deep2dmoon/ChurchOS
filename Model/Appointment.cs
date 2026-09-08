namespace htmos.model;

public class Appointment
{
    public int ID { get; set; }
    public string Appointee { get; set; } = string.Empty;
    public int BranchID { get; set; }
    public Branch Branch { get; set; } = null!;
    public DateTime EntryTime { get; set; } = DateTime.UtcNow;
}