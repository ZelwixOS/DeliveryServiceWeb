using DAL;

namespace BLL.Models
{

    public partial class CustomerModel : UserModel
    {
        public double Discount { get; set; }

        public CustomerModel(User c)
        {
            ID = c.Id;
            Email = c.Email;
            Password = c.PasswordHash;
            UserName = c.UserName;
            if (c.Discount != null)
                Discount = (double)c.Discount;
        }

        public CustomerModel()
        {

        }

    }
}
