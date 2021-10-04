using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCRUD.Entities
{
    [Table("images")]
    public class Image : BaseEntity
    {  
       
        [Display(Name = "Nome")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Url")]
        [MaxLength(300)]      
        public string Url { get; set; }

        [Display(Name = "Livros")]
        [JsonIgnore]
        public virtual Book Books { get; set; }

        [Key]
        [Display(Name = "LivrosId")]
        public int BookId { get; set; }

    }
}
