namespace CountryApp.Models
{
    public class Country
    {
        public string Id { get; set; }

        public string Name { get; set; }
        //public double CountryArea { get; set; }
        //public string? Capital { get; set; }
        public string? Region { get; set; }
        public int Population { get; set; }
        public string Flag { get; set; }
        public string Capital { get; set; }
    }
}
