using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gezifoni.Models
{
    [Table("Medyalar")]
    public class Medya
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Konum { get; set; }

        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }

        [DisplayName("Şehir")]
        public int SehirId { get; set; }

        public virtual Sehir Sehir { get; set; }
    }
}