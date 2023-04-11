using Api.Model;
using System.Collections;

namespace Api.DataAccess
{
    public interface IDataAccess
    {
        int CreateUser(User user);
        bool isEmailAvailable(string email);
        bool AuthenticateUser(string email, string password, out User user);
        IList<Book> GetAllBooks();

        bool OrderBook(int userId, int bookId);
        IList<Orders> GetOrders(int userId);

        IList<Orders>GetAllOrders();

        bool ReturnedBook(int bookId,int userId);
    }
}
