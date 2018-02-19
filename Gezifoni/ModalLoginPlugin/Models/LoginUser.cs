using Gezifoni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gezifoni.ModalLogin.Models
{
    [Table("LoginUsers")]
    public partial class LoginUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), DisplayName("Ad")]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), DisplayName("Soyad")]
        public string Surname { get; set; }

        [StringLength(60, ErrorMessage = "{0} max. {1} karakter olmalı."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("E-Posta")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Rol")]
        public string RoleName { get; set; }

        [StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Şifre")]
        public string Password { get; set; }

        [ScaffoldColumn(false), StringLength(100)]
        public string ProfileImageFileName { get; set; }

        [ScaffoldColumn(false)]
        public Guid? LostPasswordToken { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastResetPasswordDate { get; set; }

        public virtual List<Yorum> Yorumlar { get; set; }

        public LoginUser()
        {
            Yorumlar = new List<Yorum>();
        }
    }
}
