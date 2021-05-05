using System.Text.Json.Serialization;

namespace CFeClientDownloader.Core
{
    public struct SefazAPIStatus
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
