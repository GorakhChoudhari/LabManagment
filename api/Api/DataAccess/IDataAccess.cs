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
        IList<User> GetAllUsers();

        bool ReturnedBook(int bookId,int userId);

        void BlockUser(int userid);
        void UnBlockUser(int userid);
        void DeactivateUser(int userid);
        void ActivateUser(int userid);
        void InsertBook(Book book);
        bool DeleteBook(int bookId);
    }
}
