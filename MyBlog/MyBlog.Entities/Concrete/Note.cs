using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concrete
{
    [Table("Notes")]
    public class Note : BaseEntity
    {
        [Required, StringLength(50)]
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [Required, DisplayName("Metin girin")]
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public int BlogUserId { get; set; }

        // İlişkiler
        public virtual Category Category { get; set; }
        public virtual BlogUser BlogUser { get; set; }

        public virtual List<Liked> Likes { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
