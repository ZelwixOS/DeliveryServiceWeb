namespace FakeTestClasses
{
    using System;
    using System.Threading.Tasks;
    using DAL;
    using DAL.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DbInitializer
    {
        private readonly DSdb context;

        public DbInitializer(DSdb context)
        {
            this.context = context;
        }

        private async Task InitStatusesAsync(DSdb context)
        {
            if (await context.Status.FirstOrDefaultAsync(s => s.ID == 3) == null)
            {
                context.Status.Add(new Status() { ID = 1, StatusName = "Confirmed" });
                context.Status.Add(new Status() { ID = 2, StatusName = "Received" });
                context.Status.Add(new Status() { ID = 3, StatusName = "Created" });
                context.Status.Add(new Status() { ID = 4, StatusName = "Delivered" });
                context.Status.Add(new Status() { ID = 5, StatusName = "Updated" });
            }
        }

        public async Task InitDataBaseAsync(Action<DSdb> action)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            await InitStatusesAsync(context);

            action(context);
        }


    }
}
