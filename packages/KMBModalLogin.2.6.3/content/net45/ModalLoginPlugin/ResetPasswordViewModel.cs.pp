using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace $rootnamespace$.ModalLogin
{
    public class ResetPasswordViewModel
    {
        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(25, ErrorMessage = "{0} must be max. {1} characters."), Required(ErrorMessage = "{0} boş geçilemez."), DisplayName("Re-Password"), Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor."), DataType(DataType.Password)]
        public string PasswordRepeat { get; set; }
    }
}