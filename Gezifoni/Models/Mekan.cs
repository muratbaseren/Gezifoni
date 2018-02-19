using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gezifoni.Models
{
    [Table("Mekanlar")]
    public class Mekan
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Adı")]
        public string Adi { get; set; }
        public string Resim { get; set; }
        public string Adres { get; set; }

        [DisplayName("Telefon")]
        public string Tel { get; set; }

        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Yol Tarifi")]
        public string YolTarifi { get; set; }

        [DisplayName("Şehir")]
        public int SehirId { get; set; }

        public virtual Sehir Sehir { get; set; }
        public virtual List<YolTarifBirimi> YolTarifBirimleri { get; set; }

        public Mekan()
        {
            YolTarifBirimleri = new List<YolTarifBirimi>();
        }
    }
}