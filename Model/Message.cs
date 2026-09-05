using System.Text.Json.Serialization;

namespace producer.model;

public class Message
{

    public int ID { get; set; }
    public required string Type { get; set; }
    public required string Payload { get; set; }
    public MessageStatus Status { get; set; } = MessageStatus.PENDING;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageStatus
{
    PENDING, PROCESSED
}
