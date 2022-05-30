namespace BeerCollectionApi.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string BeerName { get; set; }
        public string BeerType { get; set; }
        public decimal? Rating { get; set; }
    }
}
