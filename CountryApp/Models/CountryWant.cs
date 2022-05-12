using System.ComponentModel.DataAnnotations;

namespace CountryApp.Models
{
    public class CountryWant
    {
        [Key]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
    }    
}
