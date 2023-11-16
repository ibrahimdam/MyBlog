using Microsoft.AspNetCore.Mvc;
using MyBlog.BusinessLayer;
using MyBlog.Entities.Concrete;

namespace MyBlog.WebUI.ViewComponents
{
	public class CategoriesViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			CategoryManager manager = new CategoryManager();
			// 1. yol
			List<Category> categories = manager.List();
			return View(categories);
			// 2.Yol
			//return View(manager.GetCategories());
		}
	}
}
