using Api.Model;
using System.Data.SqlClient;
using Dapper;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;
using System.Xml.Linq;
using System.Data.Common;

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

        public IList<Book> GetAllBooks()
        {
           IEnumerable<Book> books = null;
            using (var connection = new SqlConnection(Dbconnection))
            {
                var sql = "Select * from Books;";
                books = connection.Query<Book>(sql);
                foreach(var book in books)
                {
                    sql = "select * from BookCategories where Id =" + book.CategoryId;
                    book.Category = connection.QuerySingle<BookCategory>(sql);
                }

            };
            return books.ToList();
        }

        public IList<Orders> GetAllOrders()
        {
            IEnumerable<Orders> orders;
            using (var connection = new SqlConnection(Dbconnection))
            {
                var sql = @"select
                        o.Id, 
                        u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name,
                        o.BookId as BookId, b.Title as BookName,
                        o.OrderedOn as OrderDate, o.Returned as Returned
                    from Users u LEFT JOIN Orders o ON u.Id = o.UserId
                    LEFT JOIN Books b ON o.BookId = b.Id
                    where o.ID IS NOT NULL";
                orders = connection.Query<Orders>(sql);
            }
            return orders.ToList();
        }

        public IList<User> GetAllUsers()
        {
            IEnumerable<User> users;
            using (var connection = new SqlConnection(Dbconnection))
            {
                users = connection.Query<User>("select * from Users;");

                var listOfOrders =
                    connection.Query("select u.Id as UserId, o.BookId as BookId, o.OrderedOn as OrderDate, o.Returned as Returned from Users u LEFT JOIN Orders o ON u.Id=o.UserId;");

                foreach (var user in users)
                {
                    var orders = listOfOrders.Where(lo => lo.UserId == user.id).ToList();
                    var fine = 0;
                    foreach (var order in orders)
                    {
                        if (order.BookId != null && order.Returned != null && order.Returned == false)
                        {
                            var orderDate = order.OrderDate;
                            var maxDate = orderDate.AddDays(10);
                            var currentDate = DateTime.Now;

                            var extraDays = (currentDate - maxDate).Days;
                            extraDays = extraDays < 0 ? 0 : extraDays;

                            fine = extraDays * 50;
                            user.Fine += fine;
                        }
                    }
                }
            }
            return users.ToList();
        }

        public IList<Orders> GetOrders(int userId)
        {
            IEnumerable<Orders> orders;
            using (var connection = new SqlConnection(Dbconnection))
            {
                var sql = @"select
                        o.Id, 
                        u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name,
                        o.BookId as BookId, b.Title as BookName,
                        o.OrderedOn as OrderDate, o.Returned as Returned
                    from Users u LEFT JOIN Orders o ON u.Id = o.UserId
                    LEFT JOIN Books b ON o.BookId = b.Id
                    where o.UserId IN(@Id)";
                orders = connection.Query<Orders>(sql,new {id=userId});
            }
            return orders.ToList();
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

        public bool OrderBook(int userId, int bookId)
        {
            var ordered = false;
            using (var conn = new SqlConnection(Dbconnection))
            {
                var sql = $"insert into Orders(userId,BookId,OrderedOn,Returned) values ({userId},{bookId},'{DateTime.Now:yyyy-MM-dd HH:mm:ss}',0);";
                var inserted = conn.Execute(sql) == 1;
                if (inserted)
                {
                    sql = $"update Books set Ordered = 1 where Id ={bookId}";
                    var updated = conn.Execute(sql) == 1;
                    ordered = inserted;
                }

            }
            return ordered;
        }

        public bool ReturnedBook(int bookId, int userId)
        {
            var returned = false;
            using (var conn = new SqlConnection(Dbconnection)) 
            {
                var sql=$"update Books set Ordered=0 where id={bookId};";
                conn.Execute(sql);

                sql = $"update Orders set Returned=1 where UserId={userId} and BookId={bookId};";
                returned=conn.Execute(sql)==1;
            }return returned;
        }

       

        public void BlockUser(int userid)
        {
            using var conn = new SqlConnection(Dbconnection) ;
            conn.Execute("update Users Set Blocked=1 where Id = @Id",new { Id = userid });
        }

        public void UnBlockUser(int userid)
        {
            using var conn = new SqlConnection(Dbconnection);
            conn.Execute("update Users Set Blocked=0 where Id = @Id", new { Id = userid });
        }

        public void DeactivateUser(int userid)
        {
            using var conn = new SqlConnection(Dbconnection);
            conn.Execute("update Users Set Active=1 where Id = @Id", new { Id = userid });
        }

        public void ActivateUser(int userid)
        {
            using var conn = new SqlConnection(Dbconnection);
            conn.Execute("update Users Set Active=0 where Id = @Id", new { Id = userid });
        }
    }
}
