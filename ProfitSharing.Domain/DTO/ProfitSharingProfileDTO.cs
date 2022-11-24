namespace ProfitSharing.Domain.DTOs
{
    public class ProfitSharingProfileDTO
    {
        public string Name { get; set; }
        public bool IsIntern { get; set; }
        public string RegistrationNumber { get; set;}
        public decimal IndividualProfitSharingSum { get; set; }
        public int AreaWeight { get; set; }
        public int SalaryWeight { get; set; }
        public int TimeWeight { get; set; }
    }
}
