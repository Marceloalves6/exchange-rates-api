using Newtonsoft.Json;

namespace ExchangeRates.Api.Application.Contracts;

public class ServiceResponse<T>
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("processTime")]
    public ProcessTime ProcessTime { get; set; } = new();

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("result")]
    public T? Result { get; set; }

    [JsonProperty("errors")]
    public Dictionary<string, string>? Errors { get; set; }
}

public class ProcessTime
{
    [JsonProperty("startRequest")]
    public DateTime StartRequest { get; set; }

    [JsonProperty("endRequest")]
    public DateTime EndRequest { get; set; }

    [JsonProperty("executionTimeMilliseconds")]
    public double ExecutionTimeMilliseconds { get; set; }
}
