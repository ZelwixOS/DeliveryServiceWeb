using DAL;
using DAL.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FakeTestClasses
{
    class DSdbContextFactoryMock : IDatabaseContextFactory
    {
        private DbConnection connection;

        public DbContextOptions<DSdb> CreateOptions()
        {
            return new DbContextOptionsBuilder<DSdb>()
                .UseSqlite(this.connection).Options;
        }

        public DSdb CreateDbContext(string connectionString)
        {
            if (this.connection == null)
            {
                this.connection = new SqliteConnection(connectionString);
                this.connection.Open();

                var options = this.CreateOptions();
                using (var context = new DSdb(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new DSdb(this.CreateOptions());
        }

        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
            }
        }
    }
}
