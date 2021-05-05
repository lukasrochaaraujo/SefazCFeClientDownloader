using System.Text.Json.Serialization;

namespace CFeClientDownloader.Core
{
    public struct CFeDto
    {
        [JsonPropertyName("cuponId")]
        public string CupomId { get; set; }

        [JsonPropertyName("mfeId")]
        public string MFeId { get; set; }

        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("issueDate")]
        public string IssueDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; }

        [JsonPropertyName("processingStatus")]
        public string ProcessingStatus { get; set; }
    }
}
