using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concrete
{
    [Table("Likes")]
    public class Liked
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int LikedUserId { get; set; }
        // İlişkiler
        // Çoktan çoğa bir ilişki olduğundan dolayı bu ilişkili kayıtlar farklı bir tabloda tutulmak zorunda.
        public virtual Note Note { get; set; }
        public virtual BlogUser LikedUser { get; set; }
    }
}
