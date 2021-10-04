using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public class AuthorDTO
    {       
        public int Id { get; set; }
        public string Name { get; set; }
      
        public string Country { get; set; }
   
        public DateTime? BirthDate { get; set; }
    }
}
