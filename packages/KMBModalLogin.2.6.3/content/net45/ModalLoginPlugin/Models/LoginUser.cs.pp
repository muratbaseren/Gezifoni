using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $rootnamespace$.ModalLogin.Models
{
    [Table("LoginUsers")]
    public partial class LoginUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), DisplayName("Name")]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), DisplayName("Surname")]
        public string Surname { get; set; }

        [StringLength(60, ErrorMessage = "{0} must be max. {1} characters."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("E-Mail")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Username")]
        public string Username { get; set; }

        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Password")]
        public string Password { get; set; }

        [ScaffoldColumn(false), StringLength(100)]
        public string ProfileImageFileName { get; set; }

        [ScaffoldColumn(false)]
        public Guid? LostPasswordToken { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastResetPasswordDate { get; set; }
    }
}
