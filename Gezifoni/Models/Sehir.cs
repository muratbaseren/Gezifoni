using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gezifoni.Models
{
    [Table("Sehirler")]
    public class Sehir
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Adı")]
        public string Adi { get; set; }
        public string Resmi { get; set; }
        public string Slogan { get; set; }
        public string Tarihi { get; set; }

        [DisplayName("Gezilecek Yerler")]
        public string GezilecekYer { get; set; }
        public string Yemekler { get; set; }

        [DisplayName("Diğer Bilgiler")]
        public string DigerBilgiler { get; set; }

        [DisplayName("Ekleyen Üye")]
        public string EkleyenUyeAdi { get; set; }

        [DisplayName("Olusturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; }

        [DisplayName("Güncelleme Tarihi")]
        public DateTime GuncellenmeTarihi { get; set; }

        public virtual List<Medya> Medyalar { get; set; }
        public virtual List<Mekan> Mekanlar { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }

        public Sehir()
        {
            Medyalar = new List<Medya>();
            Mekanlar = new List<Mekan>();
            Yorumlar = new List<Yorum>();
        }
    }
}