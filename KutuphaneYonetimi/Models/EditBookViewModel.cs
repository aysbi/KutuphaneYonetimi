using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetimi.Models
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public DateTime BirtdayOfAuthor { get; set; }

        [Display(Name = "Title of the Book")]
        [Required(ErrorMessage = "Title name is required.")]
        public string Title { get; set; } = "";

        [Display(Name = "Author's First Name")]
        [Required(ErrorMessage = "Author's Last Name is required.")]
        public string AuthorFirstName { get; set; } = "";

        [Display(Name = "Author's Last Name")]
        [Required(ErrorMessage = "Author's Last Name is required.")]
        public string AuthorLastName { get; set; } = "";

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
