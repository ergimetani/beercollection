namespace BeerCollectionApi.Models
{
    public class BeerRatingChangeRequest
    {
        public int BeerId { get; set; }
        public decimal Rating { get; set; }
    }
}
