using Api.Model;
using System.Data.SqlClient;
using Dapper;

namespace Api.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration Configuration;
        private readonly string Dbconnection;
        public DataAccess(IConfiguration _configuration)
        {
            Configuration = _configuration;
            Dbconnection = Configuration["ConnectionStrings:DatabaseConnectionString"] ?? "";
        }

        public bool AuthenticateUser(string email, string password, out User user)
        {
           var result = false;
            using (var conn = new SqlConnection(Dbconnection))
            {
                result = conn.ExecuteScalar<bool>("Select count(1) from users where email=@email and password=@password;", new { email, password });
                if(result)
                {
                    user = conn.QueryFirst<User>("Select * from users where email=@email;", new {email});
                }
                else
                {
                    user = null;
                }
            }
            return result;
        }

        public int CreateUser(User user)
        {
            var result = 0;
            using (var conn = new SqlConnection(Dbconnection))
            {
                var parameters = new
                {
                    fn = user.FirstName,
                    ln = user.LastName,
                    em = user.Email,
                    mb = user.Mobile,
                    pwd = user.Password,
                    blk = user.Blocked,
                    act = user.Active,
                    con = user.createdOn,
                    type = user.UserType.ToString(),
                };
                var sql = "insert into Users (FirstName, LastName, Email, Mobile, Password, Blocked, Active, CreatedOn, UserType) values (@fn, @ln, @em, @mb, @pwd, @blk, @act, @con, @type);";
                result = conn.Execute(sql, parameters);
            }
            return result;
        }

        public bool isEmailAvailable(string email)
        {
           var result = false;
            using (var conn = new SqlConnection(Dbconnection))
            {
                result = conn.ExecuteScalar<bool>("select count(*) from Users where Email=@email;", new {email});
            }
             return !result;
        }
    }
}
