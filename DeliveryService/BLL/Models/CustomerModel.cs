using DAL;

namespace BLL.Models
{

    public partial class CustomerModel : UserModel
    {
        public double Discount { get; set; }

        public CustomerModel(Customer c)
        {
            ID = c.ID;
            Login = c.Login;
            Password = c.Password;
            UserName = c.UserName;
            Discount = c.Discount;
        }

        public CustomerModel()
        {

        }

    }
}
