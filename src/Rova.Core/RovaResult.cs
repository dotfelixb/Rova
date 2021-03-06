using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rova.Core
{
    public abstract class RovaResult
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }
        [JsonPropertyName("ts")]
        public long Ts { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    }

    public class SingleResult<T> : RovaResult
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }

    public class ListResult<T> : RovaResult
    {
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }

    public class ErrorResult : RovaResult { }
}

