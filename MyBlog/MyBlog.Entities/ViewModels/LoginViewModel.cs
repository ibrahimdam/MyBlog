using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage ="{0} alanı boş geçilemez"),
            StringLength(25)]
        public string UserName { get; set; }


        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(100),
            DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
