using producer.model;

namespace htmos.model;

public class Department
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string Leader { get; set; } = String.Empty;
    public List<Worker> Workers { get; set; } = [];

}