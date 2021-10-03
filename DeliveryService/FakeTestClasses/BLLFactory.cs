using BLL;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;

namespace FakeTestClasses
{
    public class BLLFactory
    {
        private readonly IDatabaseContextFactory databaseContextFactory = new DSdbContextFactoryMock();
        private DSdb context;

        private readonly string dbConnection = "DataSource=:memory:";

        public dbOperations CreateDbOperations()
        {
            if (context == null)
            {
                context = databaseContextFactory.CreateDbContext(dbConnection);
            }
            return new dbOperations(new dbReposSQL(context));
        }

        public DbInitializer CreateDbInitalizer()
        {
            if (context == null)
            {
                context = databaseContextFactory.CreateDbContext(dbConnection);
            }
            return new DbInitializer(context);
        }
    }
}
