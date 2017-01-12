using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ChangeEmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}