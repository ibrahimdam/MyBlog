using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Concrete
{
    public class MyInitialData
    {
        // Fake datalar burada hazırlanacak..

        public static void Seed()
        {
            MyBlogContext context = new MyBlogContext();

            if (context.Database.GetPendingMigrations().Count()>0) 
            {
                context.Database.Migrate();
            }
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.BlogUsers.Count() == 0)
                {
                    // Tabloda veri yok ise buradaki kodlar çalışacak.
                    // Öncelikle 2 tane user oluşturuyorum
                    BlogUser admin = new BlogUser()
                    {
                        Name = "Admin",
                        Surname = "Admin",
                        UserProfileImage = "user-profile.jpg",
                        Email = "admin@admin.com",
                        ActivateGuid = Guid.NewGuid(),
                        IsActive = true,
                        IsAdmin = true,
                        UserName = "admin",
                        Password = "123",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedUserName = "admin"
                    };

                    BlogUser standartUser = new BlogUser()
                    {
                        Name = "Mustafa",
                        Surname = "Kavusdu",
                        UserProfileImage = "user-profile.jpg",
                        Email = "mkavusdu@gmail.com",
                        ActivateGuid = Guid.NewGuid(),
                        IsActive = true,
                        IsAdmin = false,
                        UserName = "mkavusdu",
                        Password = "123",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ModifiedUserName = "mkavusdu"
                    };

                    context.BlogUsers.Add(admin);
                    context.BlogUsers.Add(standartUser);

                    for (int i = 0; i < 10; i++)
                    {
                        BlogUser user = new BlogUser()
                        {
                            Name = FakeData.NameData.GetFirstName(),
                            Surname = FakeData.NameData.GetSurname(),
                            UserProfileImage = "user-profile.jpg",
                            Email = FakeData.NetworkData.GetEmail(),
                            ActivateGuid = Guid.NewGuid(),
                            IsActive = true,
                            IsAdmin = false,
                            UserName = $"user-{i}",
                            Password = "123",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now.AddMinutes(5),
                            ModifiedUserName = $"user-{i}"
                        };
                        context.BlogUsers.Add(user);
                    }
                    context.SaveChanges();
                }

                // Kullanıcı listesini veritabanından çekeceğim ve Note, Comment gibi dataları oluştururken bu kullanıcıları kullanacağım.
                List<BlogUser> userList = context.BlogUsers.ToList();

                // Fake Kategori üreteceğim
                if (context.Categories.Count() == 0)
                {
                    // Kategoriler
                    for (int i = 0; i < 10; i++)
                    {
                        Category category = new Category()
                        {
                            Name= FakeData.PlaceData.GetCountry(),
                            Description = FakeData.PlaceData.GetAddress(),
                            CreatedDate= DateTime.Now,
                            ModifiedDate = DateTime.Now.AddMinutes(5),
                            ModifiedUserName = "admin"
                        };
                        context.Categories.Add(category);

                        // Fake note'lar eklenecek
                        for (int j = 0; j < FakeData.NumberData.GetNumber(3, 15); j++)
                        {
                            BlogUser user_note = userList[FakeData.NumberData.GetNumber(0, userList.Count-1)];

                            Note note = new Note() 
                            { 
                                Title = FakeData.PlaceData.GetCity(),
                                Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 4)),
                                Category = category,
                                IsDraft = false,
                                LikeCount = FakeData.NumberData.GetNumber(1, 12),
                                CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                                ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                                ModifiedUserName = user_note.UserName,
                                BlogUser = user_note,
                            };

                            category.Notes.Add(note);

                            // Fake yorumlar eklenecek Comment tablosuna

                            for (int k = 0; k < FakeData.NumberData.GetNumber(4,15); k++)
                            {
                                BlogUser user_comment = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                                Comment comment = new Comment()
                                {
                                    Text = FakeData.TextData.GetSentence(),
                                    BlogUser = user_comment,
                                    CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                                    ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                                    ModifiedUserName = user_comment.UserName,
                                };

                                note.Comments.Add(comment);
                            }

                            // Fake Like datası 
                            for (int m = 0;m<note.LikeCount;m++)
                            {
                                Liked liked = new Liked()
                                {
                                    LikedUser = user_note,
                                };
                                note.Likes.Add(liked);
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}


//https://codeshare.io/vwb8vD