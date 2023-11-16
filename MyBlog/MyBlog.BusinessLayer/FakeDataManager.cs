using MyBlog.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BusinessLayer
{
    public class FakeDataManager
    {
        public static void CreateFakeData()
        {
            MyInitialData.Seed();
        }
    }
}
