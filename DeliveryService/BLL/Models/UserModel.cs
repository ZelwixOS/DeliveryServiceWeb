﻿using DAL;

namespace BLL.Models
{
    public class UserModel
    {
        public UserModel(User o)
        {
            ID = o.Id;
            Email = o.Email;
            Password = o.PasswordHash;
            UserName = o.UserName;
            FirstName = o.FirstName;
            SecondName = o.SecondName;
            PhoneNumber = o.PhoneNumber;
        }
        public UserModel()
        {

        }

        public string ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
