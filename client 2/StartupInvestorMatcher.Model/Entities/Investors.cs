using Newtonsoft.Json; // For JsonProperty
using System.ComponentModel.DataAnnotations.Schema; // For Column attribute

namespace StartupInvestorMatcher.Model.Entities
{
    public class Investor
    {
        // Constructor to initialize with the associated Member ID
        public Investor(int memberId)
        {
            MemberId = memberId;
            Name_Investor = string.Empty; // Default value
            Overview_Investor = string.Empty; // Default value
            Member = new Member(memberId); // Pass the memberId to the Member constructor
        }

        // Foreign key to the Member entity
        [JsonProperty("memberId")]
        [Column("member_id")]
        public int MemberId { get; set; }

        // Name of the investor
        [JsonProperty("nameInvestor")]
        [Column("name_investor")]
        public string Name_Investor { get; set; }

        // Description or summary of the investor
        [JsonProperty("overviewInvestor")]
        [Column("overview_investor")]
        public string Overview_Investor { get; set; }

        // Country identifier (foreign key or enum reference)
        [JsonProperty("countryId")]
        [Column("country_id")]
        public int Country_Id { get; set; }

        // Industry identifier (foreign key or enum reference)
        [JsonProperty("industryId")]
        [Column("industry_id")]
        public int Industry_Id { get; set; }

        // Investment size preference identifier
        [JsonProperty("investmentSizeId")]
        [Column("investment_size_id")]
        public int Investment_Size_Id { get; set; }

        // Optional: Navigation property to Member (if using Entity Framework and needed)
        public Member Member { get; set; }
    }
}
