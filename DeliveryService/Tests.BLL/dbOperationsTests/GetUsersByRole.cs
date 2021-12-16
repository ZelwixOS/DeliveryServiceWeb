using BLL.Interfaces;
using BLL.Models;
using DAL;
using FakeTestClasses;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BLL.dbOperationsTests
{
    public class GetUsersByRoleTests
    {
        private BLLFactory factory = new BLLFactory();


        [Fact]
        public void GetUsersByRole_MoqObjectWithThreeUsers_TwoUsersOneInEachRole()
        {
            User customer = UserUtility.GetCustomer();
            User courier = UserUtility.GetCustomer();
            User admin = UserUtility.GetAdmin();

            var mock = new Mock<IAccountService>();

            mock.Setup(i => i.GetByRole("courier")).Returns(Task.FromResult((IList<User>)(new List<User> { courier })));
            mock.Setup(i => i.GetByRole("customer")).Returns(Task.FromResult((IList<User>)(new List<User> { customer })));
            mock.Setup(i => i.GetByRole("admin")).Returns(Task.FromResult((IList<User>)(new List<User> { admin })));
            var serviceMock = mock.Object;

            UsersByRole expected = new UsersByRole();
            expected.Customers.Add(new UserModel(customer));
            expected.Couriers.Add(new UserModel(courier));

            var dbOperations = factory.CreateDbOperations();


            var result = dbOperations.GetUsersByRole(serviceMock);


            Assert.Equal(expected.Couriers[0].ID, result.Couriers[0].ID);
            Assert.Equal(expected.Customers[0].ID, result.Customers[0].ID);
        }
    }
}
