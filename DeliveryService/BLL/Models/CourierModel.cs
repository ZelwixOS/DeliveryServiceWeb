using DAL;

namespace BLL.Models
{

    public partial class CourierModel: UserModel
    {
        public string CourierName { get; set; }
        public string PhoneNumber { get; set; }
        public CourierModel(Courier c)
        {
            ID = c.ID;
            Login = c.Login;
            Password = c.Password;
            UserName = c.UserName;
            PhoneNumber = c.PhoneNumber;
        }
        public CourierModel() { }
    }
}
