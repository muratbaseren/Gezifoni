using Gezifoni.ModalLogin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gezifoni.Models
{
    [Table("Yorumlar")]
    public class Yorum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Tarih { get; set; }

        [Required, DisplayName("Yorum")]
        public string YorumMetni { get; set; }

        [DisplayName("Şehir")]
        public int SehirId { get; set; }
        public int LoginUserId { get; set; }

        public virtual Sehir Sehir { get; set; }
        public virtual LoginUser LoginUser { get; set; }
    }
}