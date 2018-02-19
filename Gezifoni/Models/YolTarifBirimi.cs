using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gezifoni.Models
{
    [Table("YolTarifBirimleri")]
    public class YolTarifBirimi
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Detay { get; set; }

        public int LokasyonId { get; set; }
        public int MekanId { get; set; }

        public virtual Lokasyon Lokasyon { get; set; }
        public virtual Mekan Mekan { get; set; }
    }
}