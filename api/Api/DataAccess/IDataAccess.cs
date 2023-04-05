using Api.Model;

namespace Api.DataAccess
{
    public interface IDataAccess
    {
        int CreateUser(User user);
        bool isEmailAvailable(string email);
        bool AuthenticateUser(string email, string password, out User user);
    }
}
