using Newtonsoft.Json;

namespace NgTemplate.API.DTOs
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool Success { get; set; } = false;

        [JsonIgnore]
        public bool NotFound { get; set; } = false;

        [JsonIgnore]
        public bool InternalError { get; set; } = false;

        public dynamic Data { get; set; }

        public string Message { get; set; }
    }
}