using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;
using MyBlog.WebUI.Models;
using System.Text.Json;

namespace MyBlog.WebUI.Controllers
{
    public class UserController : Controller
    {
        BlogUserManager _userManager = new BlogUserManager();
        public IActionResult ShowProfile()
        {
            //Profil ilgili View'de gösterilecek.
            // Aşağıdaki 2 satırda, veriyi sessiondan aldık. Veri Json formatta sess,onda tutuluyordu. Bunu Deserialize yapacak BlogUser türüne çevirdik.
            //string currentUserJson = HttpContext.Session.GetString("currentUser");
            //BlogUser currentUser = JsonSerializer.Deserialize<BlogUser>(currentUserJson);

            // Veritabanından ilgili User nesnesini tekrardan istedik.
            BusinessLayerResult<BlogUser> layerResult = _userManager.GetUserById(CurrentSession.CurrentUser.Id);
            if (layerResult.Errors.Count>0)
            {
                // kullanıcıya hata mesajı gönderilecek.
            }
            return View(layerResult.Result);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            //string currentUserJson = HttpContext.Session.GetString("currentUser");
            //BlogUser currentUser = JsonSerializer.Deserialize<BlogUser>(currentUserJson);

            // Veritabanından ilgili User nesnesini tekrardan istedik.
            BusinessLayerResult<BlogUser> layerResult = _userManager.GetUserById(CurrentSession.CurrentUser.Id);
            if (layerResult.Errors.Count > 0)
            {
                // kullanıcıya hata mesajı gönderilecek.
            }
            return View(layerResult.Result);
        }
        [HttpPost]
        public IActionResult EditProfile(BlogUser user, IFormFile? file)
        {
            // Kullanıcıyı veritabanında güncelleyen kodlar yazılacak.
            // Aşağıdaki satırlar ile ilgili alanların Validation işlemlerini ModelState için yapmayacak.
            ModelState.Remove("Notes");
            ModelState.Remove("Comments");
            ModelState.Remove("Likes");
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // Parametredeki file boş gelmediyse, yani dosya geldiyse, budurumda dosyayı eşsiz bir isim ile wwwroot/images altına kaydetmemiz gerekiyor.
                    //Dosya uzantısını alalım..
                    var extension = Path.GetExtension(file.FileName);
                    //Dosya adı için eşsiz bir isim vereceğiz.
                    var imageName = string.Format($"user-{user.Id}{extension}");  // user-10.jpg gibi bir isim oluşturacak.
                    // Dosyanın kaydedileceği Path'i  ve dosya adını verelim.
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imageName);
                    // Aşağıdaki satırda da dosya adı ve yolu verilen yere fotoğrafı kaydediyoruz.
                    using(var stream =new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    user.UserProfileImage = imageName;
                }
                // Artık User nesnesini güncellemek için gerekli kodları yazabiliriz.
                
                BusinessLayerResult<BlogUser> blResult = _userManager.UpdateProfile(user);
                if (blResult.Errors.Count>0)
                {
                    // hata varsa buraya gireceğiz.
                    blResult.Errors.ForEach(x=> ModelState.AddModelError("",x));
                    return View(user);
                }
                //hata yok ise
                // Güncellenmiş kullanıcı Session'a eklenecek.
                //string currentUserJson= JsonSerializer.Serialize<BlogUser>(blResult.Result);
                //HttpContext.Session.SetString("currentUser", currentUserJson);

                CurrentSession.SetUser("currentUser", blResult.Result);
                //ilgili sayfaya yönlendirmeyi yapalım
                return RedirectToAction("ShowProfile");
            }
            return View(user);
        }
        public IActionResult DeleteProfile(int id)
        {
            // Silenecek prfilin IdSi geliyor.
            BusinessLayerResult<BlogUser> blResult = _userManager.DeleteUser(id);
            if (blResult.Errors.Count>0)
            {
                blResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                // Error sayfası tasarlanacak.
                return View("", blResult.Errors);
            }
            CurrentSession.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}



