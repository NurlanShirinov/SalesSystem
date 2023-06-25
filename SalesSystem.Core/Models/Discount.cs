namespace SalesSystem.Core.Models
{
    public class Discount : BaseModel
    {
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Percentage { get; set; }
    }
}
