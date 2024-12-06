namespace KutuphaneYonetimi.Models
{
    public class BookDetailsViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; } = "";

        public string AuthorName { get; set; } = "";

        public DateTime AuthorBirthday { get; set; }

        public string Genre { get; set; } = "";

        public DateTime PublishDate { get; set; }

        public string ISBN { get; set; }

        public int CopiesAvailable { get; set; }
    }
}
