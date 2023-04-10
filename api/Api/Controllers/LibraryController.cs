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
    }
}
