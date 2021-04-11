using DAL;

namespace BLL.Models
{

    public partial class CourierModel: UserModel
    {
        public string CourierName { get; set; }
        public string PhoneNumber { get; set; }
        public CourierModel(User c)
        {
            ID = c.Id;
            Email = c.Email;
            Password = c.PasswordHash;
            UserName = c.UserName;
            PhoneNumber = c.PhoneNumber;
        }
        public CourierModel() { }
    }
}
