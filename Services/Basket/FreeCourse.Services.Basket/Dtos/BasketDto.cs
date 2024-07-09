namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketİtemDto> BasketItems { get; set; }
        public decimal TotolPrice => BasketItems.Sum(x => x.Price);
    }
}
