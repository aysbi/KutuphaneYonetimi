using KutuphaneYonetimi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace KutuphaneYonetimi.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var booksAndAuthors = Data.Books.Join(Data.Authors, book => book.AuthorId, author => author.Id, (book, author) => new BooksAndAuthorsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                AuthorName = author.FirstName + " " + author.LastName
            }).ToList();
            return View(booksAndAuthors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create([FromForm] CreateViewModel newBook)
        {
            if (Data.Books.Any(c => c.ISBN == newBook.ISBN))
            {
                ModelState.AddModelError("", "The book is already exist.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (!Data.Authors.Any(x => x.FirstName.ToLower() == newBook.AuthorFirstName.ToLower()
                                            && x.LastName.ToLower() == newBook.AuthorLastName.ToLower()))
                    {
                        Book book = new Book()
                        {
                            Id = Data.Books.Max(c => c.Id) + 1,
                            Title = newBook.Title,
                            AuthorId = Data.Authors.Max(c => c.Id) + 1,
                            Genre = newBook.Genre,
                            PublishDate = newBook.DateOfPublish,
                            ISBN = newBook.ISBN,
                            CopiesAvailable = newBook.CopiesAvailable,
                        };

                        Data.Books.Add(book);

                        Author author = new Author()
                        {
                            Id = Data.Authors.Max(x => x.Id) + 1,
                            FirstName = newBook.AuthorFirstName,
                            LastName = newBook.AuthorLastName,
                        };

                        Data.Authors.Add(author);

                    } else
                    {
                        var existingAuthor = Data.Authors.FirstOrDefault
                            (x => x.FirstName.ToLower() == newBook.AuthorFirstName.ToLower()
                            && x.LastName.ToLower() == newBook.AuthorLastName.ToLower());

                        Book book = new Book()
                        {
                            Id = Data.Books.Max(c => c.Id) + 1,
                            Title = newBook.Title,
                            AuthorId = existingAuthor.Id,
                            Genre = newBook.Genre,
                            PublishDate = newBook.DateOfPublish,
                            ISBN = newBook.ISBN,
                            CopiesAvailable = newBook.CopiesAvailable,
                        };

                        Data.Books.Add(book);
                    }
                    return View("Feedback", newBook);
                }
            }
            return View();
        }


        public IActionResult Details(int? Id)
        {
            var book = Data.Books.FirstOrDefault(x => x.Id == Id);

            var author = Data.Authors.FirstOrDefault(x => x.Id == book.AuthorId);

            var bookDetail = new BookDetailsViewModel()
            {
                BookId = book.Id,
                Title = book.Title,
                AuthorName = author.FirstName + " " + author.LastName,
                AuthorBirthday = author.DateOfBirth,
                Genre = book.Genre,
                ISBN = book.ISBN,
                PublishDate = book.PublishDate,
                CopiesAvailable = book.CopiesAvailable
            };
            return View(bookDetail);
        }


        public IActionResult Edit(int? id)
        {
            Book? book = Data.Books.FirstOrDefault(x => x.Id == id);

            Author? author = Data.Authors.FirstOrDefault(c => c.Id == book.AuthorId);
            
            EditBookViewModel editedBook = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                Genre = book.Genre,
                AuthorId = author.Id,
                AuthorFirstName = author.FirstName,
                AuthorLastName = author.LastName
            };



            return View(editedBook);
        }

        [HttpPost]

        public IActionResult Edit([FromForm] EditBookViewModel editedBook)
        {
            if (!ModelState.IsValid)
            {
                return View(editedBook);
            }

            Book? book = Data.Books.FirstOrDefault(b => b.Id == editedBook.Id);
            Author? existAuthor = Data.Authors.FirstOrDefault(a => a.Id == book.AuthorId);

            var matchAuthor = Data.Authors.FirstOrDefault(a => a.FirstName.ToLower() == existAuthor.FirstName.ToLower() &&
                                                            a.LastName.ToLower() == existAuthor.LastName.ToLower());
            int correctAuthorId;
            DateTime birthdayAuthor;

            if (matchAuthor == null)
            {
                correctAuthorId = Data.Authors.Max(a => a.Id) + 1;
                birthdayAuthor = DateTime.Now;
            }
            else
            {
                correctAuthorId = matchAuthor.Id;
                birthdayAuthor = matchAuthor.DateOfBirth;
            }
                
            
            book.Id = editedBook.Id;
            book.AuthorId = correctAuthorId;
            book.Title = editedBook.Title;
            book.Genre = editedBook.Genre;
            book.PublishDate = editedBook.DateOfPublish;
            book.ISBN = editedBook.ISBN;
            book.CopiesAvailable = editedBook.CopiesAvailable;
            existAuthor.FirstName = editedBook.AuthorFirstName;
            existAuthor.LastName = editedBook.AuthorLastName;
            existAuthor.DateOfBirth = birthdayAuthor;
          
            return View("FeedbackEdit", editedBook);
        } 

        public IActionResult Delete(int id)
        {
            var book = Data.Books.FirstOrDefault(book => book.Id == id);
            
            return View(book);
        }

        [HttpPost]

        public IActionResult Delete2(int id)
        {
            var book = Data.Books.FirstOrDefault(book => book.Id == id);
            if(book != null)
            {
                Data.Books.Remove(book);
            }
                
            return RedirectToAction("Index");
        }
    }
}
