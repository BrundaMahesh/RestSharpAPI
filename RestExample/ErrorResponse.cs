using Newtonsoft.Json;

namespace RestExample
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string? Error {  get; set; }
    }
}