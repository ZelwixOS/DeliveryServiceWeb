using BLL;
using BLL.Models;
using DAL;
using DAL.Repositories;
using FakeTestClasses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BLL.dbOperationsTests
{
    public class UpdateOrderTests
    {
        private BLLFactory factory = new BLLFactory();

        [Fact]
        public async Task UpdateOrder_UpdatedOrder_1Async()
        {
            OrderModel order = new OrderModel() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Dest1", AdressOrigin = "Orig1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();



            int result = dbOperations.UpdateOrder(order, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrder_UpdatedOrderWrongUser_2Async()
        {
            OrderModel order = new OrderModel() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Dest1", AdressOrigin = "Orig1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
            UserModel user = new UserModel(UserUtility.GetCourier());
            int expected = 2;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();



            int result = dbOperations.UpdateOrder(order, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrder_UpdatedOrderWrongDeadline_3Async()
        {
            OrderModel order = new OrderModel() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Dest1", AdressOrigin = "Orig1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2020, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 3;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();



            int result = dbOperations.UpdateOrder(order, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateOrder_UpdatedOrderWrongId_4Async()
        {
            OrderModel order = new OrderModel() { ID = 2, AddNote = "", Cost = 120, AdressDestination = "Dest1", AdressOrigin = "Orig1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 4;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();



            int result = dbOperations.UpdateOrder(order, user);

            Assert.Equal(result, expected);
        }
    }
}
