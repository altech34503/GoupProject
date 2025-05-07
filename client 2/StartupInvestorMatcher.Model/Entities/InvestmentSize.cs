using System.Text.Json.Serialization; // For JsonPropertyName

namespace StartupInvestorMatcher.Model.Entities
{
    public class InvestmentSize
    {
        [JsonPropertyName("investment_size_id")] // Maps to "investment_size_id" in JSON
        public int InvestmentSizeId { get; set; }

        [JsonPropertyName("investment_size_name")] // Maps to "investment_size_name" in JSON
        public string? InvestmentSizeName { get; set; }
    }
}
