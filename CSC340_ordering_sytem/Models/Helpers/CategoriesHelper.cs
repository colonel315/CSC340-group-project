using System.Collections.Generic;
using System.Linq;
using CSC340_ordering_sytem.DAL;

namespace CSC340_ordering_sytem.Models.Helpers
{
    public class CategoriesHelper
    {
        private static readonly OrderingSystemDbContext Db = new OrderingSystemDbContext();

        public static ICollection<Category> GetAllCategories()
        {
            // The using statement prevents read/write errors with database. - Parsons
            // source: http://robertgreiner.com/2012/07/entity-framework-open-datareader-associated-with-command/
            using (var ctx = new OrderingSystemDbContext())
            {
                return ctx.Categories.ToList();
            }
        }
    }
}