namespace DAL
{
    using DAL.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        public DSdb CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DSdb>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DSdb(optionsBuilder.Options);
        }
    }
}
