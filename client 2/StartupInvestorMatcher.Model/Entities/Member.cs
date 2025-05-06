namespace StartupInvestorMatcher.Model.Entities;

public class Member {
    // Constructor to initialize the Member with an ID
    public Member(int memberId)
    {
        MemberId = memberId;
        MemberEmail = string.Empty;
        MemberType = string.Empty;
        MemberAddress = string.Empty;
        MemberPhone = string.Empty;
    }

    // Unique identifier for the Member
    public int MemberId { get; set; }
    
    // Email address of the Member (used for login/communication)
    public string MemberEmail { get; set; }
    
    // Type of the Member (Investor or Startup)
    public string MemberType { get; set; }
    
    // The address of the Member
    public string MemberAddress { get; set; }
    
    // Contact phone number of the Member
    public string MemberPhone { get; set; }

    // Optional: You could add more properties as needed (e.g., Overview, Country, Industry, etc.)
    // For now, let's keep it simple based on the current database schema
}