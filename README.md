# Library Management System 

## Overview 

The Library Management System is a web application designed to streamline book and author management in a library. Developed with ASP.NET Core MVC, the project incorporates Object-Oriented Programming (OOP) principles to ensure modularity and maintainability. The system supports CRUD operations for books and authors and provides intuitive user interfaces for managing library resources.

## Features

### Book Management

* <strong>Add New Books:</strong> Input book details, including title, genre, publish date, ISBN, and available copies.
* <strong>Edit Existing Books:</strong> Modify book information as needed.
* <strong>View Book Details:</strong> Display comprehensive details about a specific book.
* <strong>Delete Books:</strong> Remove books from the library system.

### Author Management

* <strong>Add New Author:</strong> Input author details, including first name, last name, and date of birth.
* <strong>Edit Existing Author:</strong> Update author information.
* <strong>View Author Details:</strong> Display detailed information about a specific author.
* <strong>Delete Authors:</strong> Remove author records from the system.

## Technology Used
* <strong>Framework:</strong> ASP.NET Core MVC
* <strong>Language:</strong> C#
* <strong>Front-end:</strong> Razor Views, HTML, CSS, JavaScript

## Models

### Book Models Properties  
| Property          | Type       | Description                                      |  
|-------------------|------------|--------------------------------------------------|  
| `Id`             | `int`      | Unique identifier for each book.                |  
| `Title`          | `string`   | Title of the book.                              |  
| `AuthorId`       | `int`      | Foreign key referencing the `Author` model.     |  
| `Genre`          | `string`   | Genre or category of the book.                  |  
| `PublishDate`    | `DateTime` | Date the book was published.                    |  
| `ISBN`           | `string`   | International Standard Book Number.             |  
| `CopiesAvailable`| `int`      | Number of copies currently available in library.|  

### Author Models Properties   
| Property          | Type       | Description                                      |  
|-------------------|------------|--------------------------------------------------|  
| `Id`             | `int`      | Unique identifier for each author.              |  
| `FirstName`      | `string`   | Author's first name.                            |  
| `LastName`       | `string`   | Author's last name.                             |  
| `DateOfBirth`    | `DateTime` | Author's date of birth.                         |  

## View Models

### Book View Models

Used to aggregate book information for listing and displaying book details.

* Combines book details with author information for a complete view.
#### Example of Book View Model
```C#
namespace KutuphaneYonetimi.Models
{
     // This ViewModel is used for creating a new book and its associated author in the system.
    public class CreateViewModel 
    {
        // Title of the book with validation attributes to ensure it's not empty.
        [Display(Name = "Title of the Book")] // The `[Display]` attribute defines the label for the field in the user interface.
        [Required(ErrorMessage = "Title name is required.")] // The `[Required]` attribute ensures that the user cannot leave this field empty.
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

        [Display(Name = "Copies Available")] // Number of available copies of the book, optional field.
        public int CopiesAvailable { get; set; } 
    }
}
 ```
### Author View Models

Used to aggregate author information for listing and displaying author details.

#### Example of Author View Model

```C#
namespace KutuphaneYonetimi.Models
{
    public class AuthorCreateViewModel
    {
        [Display(Name = "Authors First Name")] // The `[Display]` attribute defines the label for the field in the user interface.
        [Required(ErrorMessage = "Author first name is required.")] // The `[Required]` attribute ensures that the user cannot leave this field empty.
        public string FirstName { get; set; } = "";

        [Display(Name = "Authors Last Name")]
        [Required(ErrorMessage = "Author last name is required.")]
        public string LastName { get; set; } = "";

        [Display(Name = "Author's Date of Birth")]
        [Required(ErrorMessage = "Author's Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }
    }
}
```
## Conrollers

### Book Controllers

#### List: 
Fetches and displays a list of all books with authors names.

```C#
public IActionResult Index()
{
    var booksAndAuthors = Data.Books.Join(Data.Authors, book => book.AuthorId, author => author.Id, (book, author) => new BooksAndAuthorsViewModel
    {                                     // For listing books in a table with authors name, BooksAndAuthorsViewModel is created and with join structure it comes together
        Id = book.Id,
        Title = book.Title,
        AuthorId = book.AuthorId,
        AuthorName = author.FirstName + " " + author.LastName
    }).ToList(); 
    return View(booksAndAuthors); //Sending the data to the View
}
```

#### Details:
Displays detailed information about a specific book.

```C#
public IActionResult Details(int? Id) //Id : This is the identifier of the book whose details are to be displayed. 
{
    var book = Data.Books.FirstOrDefault(x => x.Id == Id);
        
    var author = Data.Authors.FirstOrDefault(x => x.Id == book.AuthorId);
        //The FirstOrDefault method ensures that either the first matching author is retrieved or null is returned if no match exists.
    var bookDetail = new BookDetailsViewModel() //book details are displayed in a new structure called BookDetailsViewModel
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
    return View(bookDetail); //new property sended to the view.
}
```

#### Create: 
Provides a form for entering new book details.

```C#
        public IActionResult Create() // this part is just for the form view
        {
            return View();
        }

        [HttpPost] // Action method to handle the form submission (POST request)

        public IActionResult Create([FromForm] CreateViewModel newBook)
        {
            if (Data.Books.Any(c => c.ISBN == newBook.ISBN)) // Check if a book with the same ISBN already exists in the data source
            {
                ModelState.AddModelError("", "The book is already exist."); // Add a model validation error if the ISBN already exists
            }
            else
            {
                if (ModelState.IsValid) // Check if the submitted form data is valid according to model validation rules
                {    // Check if the author exists in the data source (case-insensitive comparison)
                    if (!Data.Authors.Any(x => x.FirstName.ToLower() == newBook.AuthorFirstName.ToLower()
                                            && x.LastName.ToLower() == newBook.AuthorLastName.ToLower()))
                    {  // If the author does not exist, create a new book and author
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
                        // Create a new `Author` object with data from the form
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
                    return View("Feedback", newBook); // Redirect to a feedback page, passing the created book's data
                }
            }
            return View(); // If the model state is invalid or the ISBN exists, redisplay the form
        }
```
#### Edit: 
Displays a form for editing an existing book.

```C#
public IActionResult Edit(int? id) // // Action method to display the "Edit" view for a specific book
{
    Book? book = Data.Books.FirstOrDefault(x => x.Id == id); // Find the book with the given ID in the data source

    Author? author = Data.Authors.FirstOrDefault(c => c.Id == book.AuthorId); // Find the author associated with the book

    EditBookViewModel editedBook = new EditBookViewModel() // Create an instance of `EditBookViewModel` to pass data to the view
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

    return View(editedBook); // Return the "Edit" view with the pre-filled book and author data
}

[HttpPost] 

public IActionResult Edit([FromForm] EditBookViewModel editedBook)
{
    if (!ModelState.IsValid) // If the submitted form data is invalid, redisplay the form with existing data
    {
        return View(editedBook);
    }

    Book? book = Data.Books.FirstOrDefault(b => b.Id == editedBook.Id);
    Author? existAuthor = Data.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
          // Check if there's a matching author in the database based on first and last name
    var matchAuthor = Data.Authors.FirstOrDefault(a => a.FirstName.ToLower() == existAuthor.FirstName.ToLower() &&
                                                    a.LastName.ToLower() == existAuthor.LastName.ToLower());
    int correctAuthorId;
    DateTime birthdayAuthor;

    if (matchAuthor == null) // If no matching author is found, create a new author entry
    {
        correctAuthorId = Data.Authors.Max(a => a.Id) + 1;
        birthdayAuthor = DateTime.Now;
    }
    else
    {
        correctAuthorId = matchAuthor.Id; // Use the matching author's existing ID and birthday
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
  
    return View("FeedbackEdit", editedBook); // Redirect to the feedback view after successfully updating the book and author
}
```
#### Delete: 
Displays a confirmation page to delete a book.

```C#
public IActionResult Delete(int id)
{
    var book = Data.Books.FirstOrDefault(book => book.Id == id);
         // Pass the book object to the "Delete" view
    return View(book);
}

[HttpPost]

public IActionResult Delete2(int id)
{
    var book = Data.Books.FirstOrDefault(book => book.Id == id);
    if(book != null) // If the book exists in the data source, remove it
    {
        Data.Books.Remove(book); // Remove the book from the data collection
    }
        
    return RedirectToAction("Index"); // Redirect the user to the "Index" page (the list of all books)
}
```
### Author Controllers
- Very same operations with book controllers.
#### List: 
Displays a list of authors.

#### Details:
Displays detailed information about a specific author.

#### Create: 
Provides a form to add a new author.

#### Edit: 
Provides a form to edit an existing author.

#### Delete: 
Displays a confirmation page to delete an author.

## Views

### Book Views

#### ```List.cshtml```

```cshtml
@model List<BooksAndAuthorsViewModel>
@{ // Setting the title for the page (Books)
    ViewData["Title"]= "Books";
}

<h1>Books</h1>

<a class="btn btn-success" asp-area="" asp-controller="Book" asp-action="Create">Add New Book</a> 
<!-- Button to navigate to the "Create" action for adding a new book -->

<table class="table">
    <thead>
        <tr>
            <td>#ID</td>
            <td>Book Title</td>
            <td>Author Name</td>
            <td>Actions</td>
        </tr>
    </thead>
    <tbody>

        @foreach (BooksAndAuthorsViewModel bookAuthor in Model)
        {   <!-- For each bookAuthor, create a new table row -->
            <tr> 
                <td>@bookAuthor.Id</td>
                <td>@bookAuthor.Title</td>
                <td>@bookAuthor.AuthorName</td>
                       <!-- Column with action buttons for each book -->
                <td>
                                  <!-- Button to navigate to the "Details" / "Edit" / "Delete" action to view related information-->
                    <a class="btn btn-info btn-sm" asp-area="" asp-controller="Book" asp-action="Details" asp-route-id="@bookAuthor.Id">Details</a>
                    <a class="btn btn-primary btn-sm" asp-area="" asp-controller="Book" asp-action="Edit" asp-route-id="@bookAuthor.Id">Edit</a>
                    <a class="btn btn-danger btn-sm" asp-area="" asp-controller="Book" asp-action="Delete" asp-route-id="@bookAuthor.Id">Delete</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```
The rest is the basic pattern of codes.
#### ```Details.cshtml```

#### ```Create.cshtml```

#### ```Edit.cshtml```

#### ```Delete.cshtml```

#### ```Feedback.cshtml```

#### ```FeedbackEdit.cshtml```

### Author Views

Are the same pattern as Book views

## How to Run the Project

1. Clone the repository:
```
git clone <repository-url>
```
2. Open the solution in Visual Studio.
3. Build the project to restore dependencies.
4. Run the application using IIS Express or Kestrel.
5. Access the application via ```http://localhost:<port>``` in your browser.

## Future Enhancements
* Add user authentication and roles (admin, librarian, etc.).
* Enable borrowing and returning books.
* Implement a search functionality for books and authors.
