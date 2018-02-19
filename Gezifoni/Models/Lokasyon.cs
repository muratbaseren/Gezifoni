using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gezifoni.Models
{
    [Table("Lokasyonlar")]
    public class Lokasyon
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Enlem")]
        public string Latitude { get; set; }

        [Required, DisplayName("Boylam")]
        public string Longitude { get; set; }
    }
}