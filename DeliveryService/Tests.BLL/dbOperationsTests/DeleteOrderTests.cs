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
    public class DeleteOrderTests
    {
        private BLLFactory factory = new BLLFactory();

        [Fact]
        public async Task DeleteOrder_OrderIdCutomer_OneItem_1Async()
        {
            int id = 1;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
                x.TypeOfCargo.Add(new TypeOfCargo() { ID = 1, Coefficient = 0.5, Active = true, TypeName = "Type1" });
                x.OrderItem.Add(new OrderItem() { ID = 1, OrderName = "Item1", Order_ID_FK = 1, Price = 50, TypeOfCargo_ID_FK = 1 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task DeleteOrder_OrderIdCutomer_SeveralItems_1Async()
        {
            int id = 1;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
                x.TypeOfCargo.Add(new TypeOfCargo() { ID = 1, Coefficient = 0.5, Active = true, TypeName = "Type1" });
                x.OrderItem.Add(new OrderItem() { ID = 1, OrderName = "Item1", Order_ID_FK = 1, Price = 50, TypeOfCargo_ID_FK = 1 });
                x.OrderItem.Add(new OrderItem() { ID = 2, OrderName = "Item2", Order_ID_FK = 1, Price = 40, TypeOfCargo_ID_FK = 1 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }


        [Fact]
        public async Task DeleteOrder_OrderIdCutomer_NoItems_1Async()
        {
            int id = 1;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 1;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task DeleteOrder_WrongOrderIdCutomer_4Async()
        {
            int id = 2;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 4;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task DeleteOrder_OrderIdWrongCutomer_2Async()
        {
            int id = 1;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 2;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = null, Customer_ID_FK = "2", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task DeleteOrder_OrderIdCutomer_HasCourier_5Async()
        {
            int id = 1;
            UserModel user = new UserModel(UserUtility.GetCustomer());
            int expected = 5;

            await factory.CreateDbInitalizer().InitDataBaseAsync(x =>
            {
                x.Order.Add(new Order() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Destination1", AdressOrigin = "Origin1", Courier_ID_FK = "2", Customer_ID_FK = "3", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 });
            });

            var dbOperations = factory.CreateDbOperations();


            int result = dbOperations.DeleteOrder(id, user);

            Assert.Equal(result, expected);
        }
    }
}
