using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(25, ErrorMessage ="{0} alanı en fazla {2} karakter olmalı.")]
        public string UserName { get; set; }

        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(50, ErrorMessage = "{0} alanı en fazla {2} karakter olmalı."),
            EmailAddress(ErrorMessage ="Lütfen geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(100),
            DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("Şifre Tekrar"),
            Required(ErrorMessage = "{0} alanı boş geçilemez"),
            StringLength(100),
            DataType(DataType.Password),
            Compare("Password", ErrorMessage ="{0} ile {1} eşleşmiyor.")]
        public string RePassword { get; set; }
    }
}


//https://codeshare.io/0g9AYv