using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                InitDatabase(_context);
                InitData(_context);

                object _returnValue;

                /*
                 * 若使用 using 搭配 GetDbConnection 方法的話
                 *  呼叫 _context.ApplicationUser.ToArray() 方法時會出現下列錯誤
                 *  
                 * System.InvalidOperationException: 
                 * 'The ConnectionString property has not been initialized.'
                 */
                //_returnValue = CallCommandUseUsingStatement(_context);

                /*
                 * 不使用 using 搭配 GetDbConnection 方法的話
                 *  呼叫 _context.ApplicationUser.ToArray() 方法就會正確執行
                 */
                _returnValue = CallCommandNotUsingStatement(_context);

                var _users = _context.ApplicationUser.ToArray();

                Console.WriteLine();
                Console.WriteLine("current db datetime:");
                Console.WriteLine("{0:yyyy/MM/dd HH:mm:ss}", _returnValue);
                Console.WriteLine();
                Console.WriteLine("current user count:");
                Console.WriteLine($"{_users.Length}");
                Console.WriteLine();
            }

            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }


        private static void InitDatabase(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void InitData(ApplicationDbContext context)
        {
            ApplicationUser _user1 = new ApplicationUser()
            {
                Name = "boss",
                Email = "boss@txstudio.com",
                UserInRoles = new ApplicationUserInRole[] {
                        new ApplicationUserInRole() {
                            Role = new ApplicationRole() { Name = "Adminstrator" }
                        } ,
                        new ApplicationUserInRole() {
                            Role = new ApplicationRole() { Name = "Owner" }
                        } ,
                    }
            };
            ApplicationUser _user2 = new ApplicationUser()
            {
                Name = "employee1",
                Email = "employee1@txstudio.com",
                UserInRoles = new ApplicationUserInRole[] {
                        new ApplicationUserInRole() {
                            Role = new ApplicationRole() { Name = "Reader" }
                        } ,
                    }
            };

            context.ApplicationUser.Add(_user1);
            context.ApplicationUser.Add(_user2);
            context.SaveChanges();
        }

        private static object CallCommandNotUsingStatement(ApplicationDbContext context)
        {
            var _conn = context.Database.GetDbConnection();

            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn as SqlConnection;
            _cmd.CommandText = "SELECT GETDATE()";
            _cmd.CommandType = CommandType.Text;

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            var _value = _cmd.ExecuteScalar();
            return _value;
        }

        private static object CallCommandUseUsingStatement(ApplicationDbContext context)
        {
            using (var _conn = context.Database.GetDbConnection())
            {
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn as SqlConnection;
                _cmd.CommandText = "SELECT GETDATE()";
                _cmd.CommandType = CommandType.Text;

                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();

                var _value = _cmd.ExecuteScalar();

                return _value;
            }
        }
    }
}
