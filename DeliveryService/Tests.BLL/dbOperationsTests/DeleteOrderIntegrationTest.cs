using BLL;
using BLL.Models;
using DAL;
using DAL.Interfaces;
using FakeTestClasses;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BLL.dbOperationsTests
{
    public class DeleteOrderIntegrationTest
    {
        [Fact]
        public void DeleteOrder_OrderIdCutomer_OneItem_1Async()
        {
            int id = 1;
            User user = UserUtility.GetCustomer();
            UserModel userModel = new UserModel(user);

            Order order = new Order()
            { 
                ID = 1,
                AddNote = "",
                Cost = 120, AdressDestination = "Destination1",
                AdressOrigin = "Origin1",
                Courier_ID_FK = null,
                Customer_ID_FK = "3",
                Deadline = new DateTime(2021, 12, 12),
                Delivery_ID_FK = null,
                OrderDate = new DateTime(2021, 10, 6),
                ReceiverName = "user1",
                Status_ID_FK = 0
            };

            TypeOfCargo typeOfCargo = new TypeOfCargo() { ID = 1, Coefficient = 0.5, Active = true, TypeName = "Type1" };

            OrderItem orderItem = new OrderItem() { 
                ID = 1,
                OrderName = "Item1",
                Order_ID_FK = 1,
                Price = 50, 
                TypeOfCargo_ID_FK = 1,
                TypeOfCargo = typeOfCargo
            };

            int expected = 1;


            var mock = new Mock<IdbOperations>();

            mock.Setup(i => i.Orders.GetItem(id)).Returns(order);
            mock.Setup(i => i.OrderItems.GetList()).Returns(new List<OrderItem> { orderItem });
            mock.Setup(i => i.OrderItems.GetItem(orderItem.ID)).Returns(orderItem);
            mock.Setup(i => i.TypesOfCargo.GetItem(typeOfCargo.ID)).Returns(typeOfCargo);
            mock.Setup(i => i.Users.GetItem(user.Id)).Returns(user);
            mock.Setup(i => i.OrderItems.Delete(It.IsAny<int>()));
            mock.Setup(i => i.Orders.Delete(1));
            mock.Setup(i => i.Save()).Returns(1);

            var dbContext = mock.Object;

            var dbOperations = new dbOperations(dbContext);

            int result = dbOperations.DeleteOrder(id, userModel);

            Assert.Equal(result, expected);
        }
    }
}
