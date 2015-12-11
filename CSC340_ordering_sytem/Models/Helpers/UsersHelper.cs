using CSC340_ordering_sytem.DAL;
using System.Linq;

namespace CSC340_ordering_sytem.Models.Helpers
{
    public class UsersHelper
    {
        private static readonly OrderingSystemDbContext Db = new OrderingSystemDbContext();

        public static User GetUserById(int id)
        {
            return Db.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public static bool IsUserAdmin(int id)
        {
            var user = Db.Users.Find(id);
            return user != null && (user.Role.Equals("Admin"));
        }
    }
}