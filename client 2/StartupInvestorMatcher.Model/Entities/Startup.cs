namespace StartupInvestorMatcher.Model.Entities
{
    public class Startup
    {
        // Constructor to initialize the Startup with its Member ID
        public Startup(int memberId)
        {
            MemberId = memberId;
            NameStartup = string.Empty; // Initialize with a default value
            OverviewStartup = string.Empty; // Initialize with a default value
        }

        // The ID from the Member table (acts as a foreign key and identifier)
        public int MemberId { get; set; }

        // Name of the startup
        public string NameStartup { get; set; }

        // Overview or description of the startup
        public string OverviewStartup { get; set; }

        // Foreign key: Country ID (e.g., referencing a Country table)
        public int CountryId { get; set; }

        // Foreign key: Industry ID (e.g., referencing an Industry table)
        public int IndustryId { get; set; }

        // Foreign key: Investment Size ID (e.g., referencing an InvestmentSize table)
        public int InvestmentSizeId { get; set; }
    }
}
