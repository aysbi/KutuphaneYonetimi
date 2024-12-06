using KutuphaneYonetimi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace KutuphaneYonetimi.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            var authorsList = Data.Authors.ToList();

            return View(authorsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create([FromForm] AuthorCreateViewModel newAuthor)
        {
            if(Data.Authors.Any(c => c.FirstName.ToLower() == newAuthor.FirstName.ToLower() &&
                                    c.LastName.ToLower() == newAuthor.FirstName.ToLower()))
            {
                ModelState.AddModelError("", "The author is already exist.");
            }

            if(ModelState.IsValid)
            {
                Author author = new Author()
                {
                    Id = Data.Authors.Max(c => c.Id) + 1,
                    FirstName = newAuthor.FirstName,
                    LastName = newAuthor.LastName,
                    DateOfBirth = newAuthor.DateOfBirth
                };

                Data.Authors.Add(author);
            }
            return View("CreatingAuthorFeedback", newAuthor);
        }

        public IActionResult Details(int? Id)
        {
            var author = Data.Authors.FirstOrDefault(x => x.Id == Id);

            var authorDetails = new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName, 
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth
            };
            return View(authorDetails);
        }

        public IActionResult Edit(int? id)
        {
            Author? author = Data.Authors.FirstOrDefault(c => c.Id == id);

            if(author == null)
            {
                return NotFound();
            }

            EditAuthorViewModel editedAuthor = new EditAuthorViewModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth
            };

            return View(editedAuthor);
        }

        [HttpPost]

        public IActionResult Edit([FromForm] EditAuthorViewModel editedAuthor)
        {
            if (!ModelState.IsValid)
            {
                return View(editedAuthor);
            }

            Author? author = Data.Authors.FirstOrDefault(a => a.Id == editedAuthor.Id);

            var matchAuthor = Data.Authors.FirstOrDefault(a => a.FirstName.ToLower() == author.FirstName.ToLower() &&
                                                            a.LastName.ToLower() == author.LastName.ToLower());
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

            author.Id = correctAuthorId;
            author.FirstName = editedAuthor.FirstName;
            author.LastName = editedAuthor.LastName;
            author.DateOfBirth = birthdayAuthor;

            return View("AuthorEditFeedback", editedAuthor);
        }

        public IActionResult Delete(int id)
        {
            var author = Data.Authors.FirstOrDefault(author => author.Id == id);

            return View(author);
        }

        [HttpPost]

        public IActionResult Delete2(int id)
        {
            var author = Data.Authors.FirstOrDefault(author => author.Id == id);
            if (author != null)
            {
                Data.Authors.Remove(author);
            }

            return RedirectToAction("Index");
        }

    }
}
