using System.Text.Json.Serialization;

namespace StartupInvestorMatcher.Model.Entities
{
    public class Investor
    {
        [JsonPropertyName("id")]
        public int MemberId { get; set; }

        [JsonPropertyName("name_Investor")]
        public string NameInvestor { get; set; } = string.Empty;

        [JsonPropertyName("overview_Investor")]
        public string OverviewInvestor { get; set; } = string.Empty;

        [JsonPropertyName("country_Id")]
        public int CountryId { get; set; }

        [JsonPropertyName("industry_Id")]
        public int IndustryId { get; set; }

        [JsonPropertyName("investment_Size_Id")]
        public int InvestmentSizeId { get; set; }
    }
}
