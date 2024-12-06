using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetimi.Models
{
    public class CreateViewModel
    {
        [Display(Name = "Title of the Book")]
        [Required(ErrorMessage = "Title name is required.")]
        public string Title { get; set; } = "";

        [Display(Name = "Authors First Name")]
        [Required(ErrorMessage = "Author first name is required.")]
        public string AuthorFirstName { get; set; } = "";

        [Display(Name = "Authors Last Name")]
        [Required(ErrorMessage = "Author last name is required.")]
        public string AuthorLastName { get; set; } = "";

        [Display(Name = "Author's Date of Birth")]
        [Required(ErrorMessage = "Author's Date of Birth is required.")]
        public DateTime AuthorDateOfBirth { get; set; } 

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = "";

        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "ISBN is required.")]
        public string ISBN { get; set; } = "";

        [Display(Name = "Date of Publish")]
        [Required(ErrorMessage = "Date of Publish is required.")]
        public DateTime DateOfPublish { get; set; }

        [Display(Name = "Copies Available")]
        public int CopiesAvailable { get; set; } 

    }
}
