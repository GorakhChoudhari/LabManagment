using Api.DataAccess;
using Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IDataAccess library;
        private readonly IConfiguration configuration;
        public LibraryController(IDataAccess lab, IConfiguration configuration)
        {
            this.library = lab;
            this.configuration = configuration;
        }
        [HttpPost("CreateAccount")]
        public IActionResult createAccouny(User user)
        {
            if (!library.isEmailAvailable(user.Email))
             {
                return Ok("Email is not Available");
            }
            user.createdOn=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            user.UserType = UserType.USER;
            library.CreateUser(user);
            return Ok("Account created Successfully");
        }
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            if (library.AuthenticateUser(email, password, out User? user))
            {
                if(user != null)
                {
                    var jwt = new Jwt(configuration["Jwt:key"], configuration["Jwt:Duration"]);
                    var Token = jwt.GenerateTokens(user);
                    return Ok(Token);
                }
            }
            return Ok("Inavlid");
        }

        [HttpGet("GetAllBooks")]

        public IActionResult GetAllBooks()
        {
            var books = library.GetAllBooks();
            var booksToSend = books.Select(book => new
            {
                book.id,
                book.Title,
                book.Category.Category,
                book.Category.SubCategory,
                book.Price,
                book.Author,
                Available = !book.Ordered

            }).ToList();
            return Ok(booksToSend);
        }
        [HttpGet("OrderBook/{userId}/{bookId}")]
        public IActionResult OrderBook(int userId,int bookId)
        {
            var result = library.OrderBook(userId, bookId) ? "Success" : "Fail";
            return Ok(result);
        }
        [HttpGet("GetOrders/{userId}")]

        public IActionResult Getorders(int userId)
        {
            var result = library.GetOrders(userId);
            return Ok(result);  
        }
        [HttpGet("GetOrders")]

        public IActionResult GetAllOrders()
        {
            var result = library.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("ReturnBook/{bookId}/{userId}")]
        public IActionResult ReturedBook(string bookId,string userId)
        {
            var result =library.ReturnedBook(int.Parse(bookId),int.Parse(userId));
            return Ok(result == true ? "Not Returned" : "Success");
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = library.GetAllUsers();
            var result = users.Select(user => new
            {
                user.id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Mobile,
                user.Blocked,
                user.Active,
                user.createdOn,
                user.UserType,
                user.Fine,
            });

            return Ok(result);
        }

        [HttpGet("ChangeBlockStatus/{status}/{id}")]
        public IActionResult ChangeBlockStatus(int status , int id) {
            if (status == 1)
            {
                library.BlockUser(id);
            }
            else {
                library.UnBlockUser(id);
            }
            return Ok("Success");
        }

        [HttpGet("ChangeEnableStatus/{status}/{id}")]
        public IActionResult ChangeEnableStatus(int status, int id)
        {
            if (status == 1)
            {
                library.DeactivateUser(id);
            }
            else
            {
                library.ActivateUser(id);      
                    
             };
            return Ok("Success");
        }
        [HttpPost("InsertBook")]
        public IActionResult InsertBook(Book book)
        {
            book.Title = book.Title.Trim();
            book.Author = book.Author.Trim();
            book.Category.Category = book.Category.Category.ToLower();
            book.Category.SubCategory = book.Category.SubCategory.ToLower();


            library.InsertBook(book);
            return Ok("Inserted");


        }
        [HttpDelete("deleteBook")]

        public IActionResult DeleteBook(int bookId)
        {
            var result = library.DeleteBook(bookId) ? "success" : " Fail ";
            return Ok(result);
        }
    }
}
