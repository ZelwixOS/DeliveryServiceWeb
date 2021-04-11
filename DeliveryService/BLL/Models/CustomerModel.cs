using DAL;

namespace BLL.Models
{

    public partial class CustomerModel : UserModel
    {
        public double Discount { get; set; }

        public CustomerModel(Customer c)
        {
            ID = c.Id;
            Email = c.Email;
            Password = c.PasswordHash;
            UserName = c.UserName;
            Discount = c.Discount;
        }

        public CustomerModel()
        {

        }

    }
}
