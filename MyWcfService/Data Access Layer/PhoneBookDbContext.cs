using PhoneBookService.EntityModels;
using System.Data.Entity;

namespace PhoneBookService.Data_Access_Layer
{
    internal class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<PhoneBookEntity> PhoneBookEntities { get; set; }
    }
}