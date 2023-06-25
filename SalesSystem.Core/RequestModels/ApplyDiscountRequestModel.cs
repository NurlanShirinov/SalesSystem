namespace SalesSystem.Core.RequestModels
{
    public class ApplyDiscountRequestModel
    {
        public Guid DiscountId { get; set; }
        public List<Guid> ProductId { get; set; } 
    }
}
