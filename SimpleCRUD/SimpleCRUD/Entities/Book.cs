

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleCRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace SimpleCRUD.Entities
{
    [Table("books")]
    public class Book:BaseEntity
    {
       
        [Key]
        [Display(Name = "Título")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Key]
        [Display(Name = "Subtítulo")]
        [MaxLength(250)]
        public string SubTitle { get; set; }

        [Key]
        [Display(Name = "Publicação")]
        [Column("publication_date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]     
        public DateTime PublicationDate { get; set; }

        [Key]
        [Display(Name = "Autores")]
        public virtual List<Author> Author { get; set; }       


        [Key]
        [Display(Name = "Categoria")]        
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeCategory Category { get; set; }

        [Key]
        [Display(Name = "Imagem")]
        public virtual List<Image> Image { get; set; }

       

    }
}
