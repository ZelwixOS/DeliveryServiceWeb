using BLL;
using BLL.Models;
using FakeTestClasses;
using System;
using Xunit;

namespace Tests.BLL.dbOperationsTests
{
    public class UpdateOrderTests
    {
        private dbOperations testingClass = new dbOperations(new CrudUoW());

        [Fact]
        public void UpdateOrder_UpdatedOrder_1()
        {
            OrderModel order = new OrderModel() { ID = 1, AddNote = "", Cost = 120, AdressDestination = "Dest1", AdressOrigin = "Orig1", Courier_ID_FK = "2", Customer_ID_FK = "1", Deadline = new DateTime(2021, 12, 12), Delivery_ID_FK = null, OrderDate = new DateTime(2021, 10, 6), ReceiverName = "user1", Status_ID_FK = 0 };
            UserModel user = new UserModel() { ID = "1", Email="email@mail.com", FirstName="First", SecondName="Second", Password="ABCD123456!", UserName="user1", PhoneNumber="0000000000" };
            int expected = 1;

            int result = testingClass.UpdateOrder(order, user);

            Assert.Equal(result, expected);
        }
    }
}
