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
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Required, DisplayName("Kategori Adı"), StringLength(50)]
        public string Name { get; set; }
        [StringLength(200), DisplayName("Açıklama")]
        public string Description { get; set; }
        // İlişkiler
        public virtual List<Note> Notes { get; set; }

        public Category()
        {
            Notes = new List<Note>();
        }
    }
}

// Data Annotations'ları eklememiz gerekiyor.
