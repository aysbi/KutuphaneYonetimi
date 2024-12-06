namespace KutuphaneYonetimi.Models
{
    public static class Data
    {
        public static List<Book> Books { get; set; } = new()
        {
            new Book { Id = 1, Title = "Hyunam-dong Store", AuthorId = 1, Genre = "Fiction", PublishDate = new DateTime(2022, 8, 15), ISBN = "9788972758045", CopiesAvailable = 10 },
            new Book { Id = 2, Title = "Book Lovers", AuthorId = 2, Genre = "Romance", PublishDate = new DateTime(2022, 5, 3), ISBN = "9780593334836", CopiesAvailable = 15 },
            new Book { Id = 3, Title = "The Housemaid", AuthorId = 3, Genre = "Thriller", PublishDate = new DateTime(2022, 1, 18), ISBN = "9781538742570", CopiesAvailable = 7 },
            new Book { Id = 4, Title = "Caraval", AuthorId = 4, Genre = "Fantasy", PublishDate = new DateTime(2017, 1, 31), ISBN = "9781250095251", CopiesAvailable = 12 },
            new Book { Id = 5, Title = "Never Lie", AuthorId = 3, Genre = "Psychological Thriller", PublishDate = new DateTime(2022, 9, 20), ISBN = "9781234567890", CopiesAvailable = 11 },
            new Book { Id = 6, Title = "The Vegetarian", AuthorId = 5, Genre = "Literary Fiction", PublishDate = new DateTime(2007, 10, 30), ISBN = "9781846276033", CopiesAvailable = 8}
        };

        public static List<Author> Authors = new()
        {
            new Author { Id = 1, FirstName = "Soon-Young", LastName = "Lee", DateOfBirth = new DateTime(1980, 9, 15) },
            new Author { Id = 2, FirstName = "Emily", LastName = "Henry", DateOfBirth = new DateTime(1991, 3, 21) },
            new Author { Id = 3, FirstName = "Freida", LastName = "McFadden", DateOfBirth = new DateTime(1979, 10, 1) },
            new Author { Id = 4, FirstName = "Stephanie", LastName = "Garber", DateOfBirth = new DateTime(1982, 12, 29) },
            new Author { Id = 5, FirstName = "Han", LastName = "Kang", DateOfBirth = new DateTime (1970, 11, 27)}
        };
    }
}
