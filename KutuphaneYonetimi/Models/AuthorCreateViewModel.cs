using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetimi.Models
{
    public class AuthorCreateViewModel
    {
        [Display(Name = "Authors First Name")]
        [Required(ErrorMessage = "Author first name is required.")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Authors Last Name")]
        [Required(ErrorMessage = "Author last name is required.")]
        public string LastName { get; set; } = "";

        [Display(Name = "Author's Date of Birth")]
        [Required(ErrorMessage = "Author's Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }
    }
}
