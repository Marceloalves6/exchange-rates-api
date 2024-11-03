using System.Text.Json.Serialization;

namespace ExchangeRates.Api.Application.Contracts;

public class ServiceResponse<T>
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("processTime")]
    public ProcessTime ProcessTime { get; set; } = new();

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("result")]
    public T? Result { get; set; }

    [JsonPropertyName("errors")]
    public List<KeyValuePair<string, string>>? Errors { get; set; }
}

public class ProcessTime
{
    [JsonPropertyName("startRequest")]
    public DateTime StartRequest { get; set; }

    [JsonPropertyName("endRequest")]
    public DateTime EndRequest { get; set; }

    [JsonPropertyName("executionTimeMilliseconds")]
    public double ExecutionTimeMilliseconds { get; set; }
}
