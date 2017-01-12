using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ChangePhoneNumberViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}