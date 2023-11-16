using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;
using MyBlog.WebUI.Models;

namespace MyBlog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        CommentManager _commentManager = new CommentManager();
        public IActionResult ShowNoteComments(int? id)
        {
            // İd'ye göre veritabanından yorumları sorgulayacağım
            if (id == null)
            {
                return BadRequest();
            }

            List<Comment> comments = _commentManager.ListQueryable().Include("BlogUser").Where(c => c.NoteId == id).ToList();
            if (comments == null)
            {
                return NotFound();
            }
            return PartialView("_PartialComments", comments);
        }
        [HttpPost]
        public IActionResult Edit(int id, string text)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Comment comment = _commentManager.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Text = text;
            comment.ModifiedDate = DateTime.Now;
            if (_commentManager.Update(comment)>0)
            {
                return Json(new { result = true});
            }
            return Json(new { result = false });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Comment comment = _commentManager.GetById(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            if(_commentManager.Delete(comment)>0)
            {
                return Json(new { result = true});
            }
            
            return Json(new { result = false });           

        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            if (comment.Id == null)
            {
                return BadRequest();
            }
            comment.CreatedDate= DateTime.Now;
            comment.ModifiedDate= DateTime.Now;
            comment.BlogUserId = CurrentSession.CurrentUser.Id;
            comment.ModifiedUserName = CurrentSession.CurrentUser.UserName;


            if(_commentManager.Insert(comment)>0)
            {
                return Json(new { result = true });
            }
            return Json(new { result = false });



        }
    }
}
