using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LoginViewModel
    {
            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
            [Display(Name = "Запомнить?")]
            public bool RememberMe { get; set; }
            public string ReturnUrl { get; set; }
    }
}
