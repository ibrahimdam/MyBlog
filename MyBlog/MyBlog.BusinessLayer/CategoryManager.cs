using Microsoft.EntityFrameworkCore;
using MyBlog.DataAccessLayer.Concrete;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BusinessLayer
{
    public class CategoryManager : BaseManager<Category>
    {
        public Category GetCategoryByIdWithNotes(int id)
        {
            var cat = base.ListQueryable(x=> x.Id== id).Include(x=> x.Notes.OrderByDescending(x=>x.ModifiedDate)).FirstOrDefault();

            return cat;
        }

        public int InsertCategory(Category category, string userName)
        {
            category.CreatedDate = DateTime.Now;
            category.ModifiedDate = DateTime.Now;
            category.ModifiedUserName = userName;

           return Insert(category);
        }


    }
}
