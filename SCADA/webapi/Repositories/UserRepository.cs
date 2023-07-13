using webapi.Enum;
using webapi.model;

namespace webapi.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserByCredentials(string username,string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ScadaDBContext _context;

        public UserRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public User GetUserByCredentials(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
