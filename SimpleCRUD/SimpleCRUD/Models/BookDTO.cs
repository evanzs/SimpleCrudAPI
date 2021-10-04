using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }      
        public string SubTitle { get; set; }      
        public DateTime PublicationDate { get; set; }
     
        [JsonConverter(typeof(StringEnumConverter))]
        [EnumDataType(typeof(TypeCategory))]
        public TypeCategory Category { get; set; }

        public List<int> AuthorIds { get; set; }


    }
}
