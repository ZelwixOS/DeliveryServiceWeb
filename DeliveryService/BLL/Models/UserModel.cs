using DAL;

namespace BLL.Models
{
    public class UserModel
    {
        public UserModel(User o)
        {
            ID = o.ID;
            Login = o.Login;
            Password = o.Password;
            UserName = o.UserName;
        }
        public UserModel()
        {

        }

        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
