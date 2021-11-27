using BLL.Models;
using DAL;
using FakeTestClasses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BLL.dbOperationsTests
{
    public class UpdateOrderStatusTests
    {
        private BLLFactory factory = new BLLFactory();

        [Fact]
        public async Task UpdateOrderStatus_ValidDataClient_1()
        {
            int id = 1;
            int status = 1;
            string role = "customer";
            UserModel user = new UserModel(UserUtility.GetCustomer());

            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = id, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();
                

            int result = dbOperations.UpdateOrderStatus(id, status, role, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrderStatus_WrongId_4()
        {
            int id = 1;
            int status = 1;
            string role = "customer";
            UserModel user = new UserModel(UserUtility.GetCustomer());

            int expected = 4;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 2, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.UpdateOrderStatus(id, status, role, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrderStatus_WrongRole_2()
        {
            int id = 1;
            int status = 1;
            string role = "admin";
            UserModel user = new UserModel(UserUtility.GetCustomer());

            int expected = 2;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = id, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.UpdateOrderStatus(id, status, role, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrderStatus_NotClientsOrder_2()
        {
            int id = 1;
            int status = 1;
            string role = "customer";
            UserModel user = new UserModel(UserUtility.GetCustomer());

            int expected = 2;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = id, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "1", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.UpdateOrderStatus(id, status, role, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrderStatus_ValidDataCourier_1()
        {
            int id = 1;
            int status = 1;
            string role = "courier";
            UserModel user = new UserModel(UserUtility.GetCourier());

            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = id, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.UpdateOrderStatus(id, status, role, user);

            Assert.Equal(result, expected);
        }
    }
}
