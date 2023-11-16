using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.ViewModels;
using MyBlog.WebUI.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MyBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();
        NoteManager _noteManager = new NoteManager();
        BlogUserManager _blogUserManager = new BlogUserManager();
        public IActionResult Index()
        {
            //FakeDataManager.CreateFakeData();

            return View(_noteManager.List().OrderByDescending(x => x.ModifiedDate).ToList());
        }

        public IActionResult SelectCategory(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // HttpStatusCode 
            }
            Category category = _categoryManager.GetCategoryByIdWithNotes(id.Value);
            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Index", category.Notes);
        }


        public IActionResult MostLiked()
        {
            return View("Index", _noteManager.List().OrderByDescending(x => x.LikeCount).ToList());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Girilen bilgilerin kontrolü yapılacak..
            // Bilgiler doğru ise Index sayfasına yönlendirilecek.
            // kullanıcı adını session'da saklanması gibi..
            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> blResult = _blogUserManager.Login(model);
                if (blResult.Errors.Count > 0)
                {
                    blResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                // Kullanıcı session'a eklenecek. Önce Json'a çeviriyorum.
                //string currentUserJson = JsonSerializer.Serialize<BlogUser>(blResult.Result); 
                //HttpContext.Session.SetString("currentUser", currentUserJson);
                CurrentSession.SetUser("currentUser", blResult.Result);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            CurrentSession.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            // Verilerin doğruluğu kontrol edilir. Validation işlemleri
            // Validation işleminden geçerse veritabanına kaydı yapılacak.
            // E-posta gönderilecek (Aktivasyon için)

            if (ModelState.IsValid)
            {
                #region Eski kodlar
                // Aşağıda yapılan karşılaştırmalar UI tarafında yapılmaz. Bu işler Bussines layer tarafında ypılması gereken işlerdir.
                // Aşağıdaki kodlar, diğer kodların çalışıp çalışmadığını test amaçlı yazdık.
                //bool hasError= false;
                //if (model.UserName == "aaa")
                //{
                //	ModelState.AddModelError("","Kullanıcı adı kullanılıyor..");
                //	hasError= true;
                //}
                //if (model.Email == "abc@abc.com")
                //{
                //	ModelState.AddModelError("","Girdiğiniz e-posta başkası tarafından kullanılıyor.");
                //                hasError = true;
                //            }

                //if (hasError)
                //{
                //	//
                //	return View(model);

                //}
                #endregion
                BusinessLayerResult<BlogUser> blResult = _blogUserManager.RegisterUser(model);
                if (blResult.Errors.Count > 0)
                {
                    // hataları ekranda göster.. 
                    blResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    // AddModelError içindeki hata mesajlarını ekranda görebiliyorum ama BusinessLayerResult'ta Errors List'ten gelen hata mesajlarını göremiyorum.
                    // Yukarıdaki kod ile Errors List'teki hataları ModelState.AddModelError içine ekleyerek, hata mesajlarının ekranda görünmesini sağlıyorum.

                    return View(model);
                }


                return RedirectToAction("RegisterSuccess");
            }


            return View(model);

        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        public IActionResult UserActivate(Guid id)
        {
            //Mail hesabına gelen link ile çalışacak olan Action burasıdır.
            BusinessLayerResult<BlogUser> blResult = _blogUserManager.UserActivate(id);

            if (blResult.Errors.Count > 0)
            {
                TempData["errors"] = blResult.Errors;
                return RedirectToAction("ActivateUserCancel");
            }
            return RedirectToAction("ActivateUserOk");
        }

        public IActionResult ActivateUserOk()
        {
            return View();
        }
        public IActionResult ActivateUserCancel()
        {
            List<string> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<string>;
            }
            return View(errors);
        }

            





        public IActionResult About()
        {
            return View();
        }

    }
}

