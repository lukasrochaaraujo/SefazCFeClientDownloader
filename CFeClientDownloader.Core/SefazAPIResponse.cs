using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFeClientDownloader.Core
{
    public struct SefazAPIResponse
    {
        [JsonPropertyName("status")]
        public SefazAPIStatus Status { get; set; }

        [JsonPropertyName("total")]
        public int TotalCFeItems { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<CFeDto> CFeCollection { get; set; }
    }
}
