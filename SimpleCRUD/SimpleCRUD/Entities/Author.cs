
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCRUD.Entities
{
    [Table("authors")]
    public class Author:BaseEntity 
    {
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }        
            
        [Column("birth_date")]      
        public DateTime? BirthDate { get; set; }                
      
        [JsonIgnore]
        public virtual List<Book> Books { get; set; }

    }
}
